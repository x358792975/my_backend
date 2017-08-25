

//This is a sample for database connection 
var express = require('express');
var app = express();
var dateFormat = require('dateformat');

function recordsetToJson(data){
	return JSON.stringify(data);
}

app.get('/', function(req, res){
var sql = require('mssql');

	var config = {
    "user": 'sa',
    "password": 'PASSWORD',
    "server": 'SEAN',
    "database": 'DATABASE ',
    "dialect": "mssql",
    "dialectOptions": {
        "instanceName": "YOUR INSTANCE NAME"
    }

	};

	sql.connect(config, function(err){
		if(err) console.log(err);

		//dateFormat.masks.hammerTime = 'mm-dd-yyyy ';
		var request = new sql.Request(); 

//(convert(varchar(11),stock_date))
		//var myQ = 'SELECT stock_name　FROM mydb.dbo.stock_table';
		request.query('SELECT ticker, stock_date, openP,  high, low, closeP, volumn 　FROM mydb.dbo.stock_table', function(err, recordset){
			if(err) console.log(err);

			res.send(recordset);
			console.log(recordsetToJson(recordset));
		});
	sql.close();			
	});

});

var server = app.listen(5000, function(){
	 console.log('Server is running');
});