using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;

namespace EntityCreator.PgSql;

public interface IPgsql: IAsyncDisposable {
	Task<List<pColumn>> GetColumnsAsync(string tableName, string schemaName = "public");
}
public class Pgsql: IPgsql {

	private readonly ILogger<Pgsql> _logger;
	private ConnectionState _connectionState = ConnectionState.Closed;
	private readonly NpgsqlConnection _connection;
	
	public Pgsql(IOptions<DbSettings> dbSettings, ILogger<Pgsql> logger){
		_logger = logger;
		_connection = new NpgsqlConnection(dbSettings.Value.ConnectionString);
		_connection.StateChange += OnConnectionStateChange;
	}
	
	private void OnConnectionStateChange(object sender, StateChangeEventArgs e){
		_logger.LogInformation(new EventId(100, "StateChange"), "State changed: \u001b[1;33m{0}\u001b[0m -> \u001b[1;33m{1}\u001b[0m", e.OriginalState, e.CurrentState);
		_connectionState = e.CurrentState;
	}
	
	public async Task<List<pColumn>> GetColumnsAsync(string tableName, string schemaName = "public"){
		if(_connection.State == ConnectionState.Closed){
			await _connection.OpenAsync();
		}
		const string cmdText = """
							SELECT
									columns.column_name, 
									columns.data_type, 
									columns.is_nullable, 
									columns.column_default, 
									columns.character_maximum_length,
									columns.numeric_precision, 
									columns.numeric_scale
							FROM 
								information_schema.columns AS columns
							WHERE 
								columns.table_schema = @schemaName
								AND columns.table_name = @tableName
							ORDER BY 
								columns.ordinal_position;
							""";
		await using NpgsqlCommand command = new (cmdText, _connection);
		command.Parameters.AddWithValue("tableName", tableName);
		command.Parameters.AddWithValue("schemaName", schemaName);
		await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
		
		List<pColumn> columns = [];
		while (await reader.ReadAsync()){
			string columnName = await reader.GetFieldValueAsync<string>(0);
			string dataType = await reader.GetFieldValueAsync<string>(1);
			bool isNullable = await reader.GetFieldValueAsync<string>(2) == "YES";
			string? columnDefault = await reader.IsDBNullAsync(3) ? null : await reader.GetFieldValueAsync<string>(3);
			int? characterMaximumLength = await reader.GetFieldValueAsync<int?>(4);
			int? numericPrecision = await reader.GetFieldValueAsync<int?>(5);
			int? numericScale = await reader.GetFieldValueAsync<int?>(6);
			columns.Add(new pColumn(columnName, dataType, isNullable, columnDefault, characterMaximumLength, numericPrecision, numericScale));
		}
		return columns;
	}

	public async ValueTask DisposeAsync(){
		if(_connection.State == ConnectionState.Open){
			await _connection.CloseAsync();
		}
		_connection.StateChange -= OnConnectionStateChange;
		await _connection.DisposeAsync();
	}
}

public record pColumn(string ColumnName, string DataType, bool IsNullable, 
	string? ColumnDefault, int? CharacterMaximumLength, int? NumericPrecision, int? NumericScale);