var sql = require('mssql');

var config = {
    server: 'localhost',
    database: 'database',
    user: 'sa',
    password: 'password',
};

function loadEmployees() {
   
    var dbConn = new sql.Connection(config);
    
    dbConn.connect().then(function () {
        
        var request = new sql.Request(dbConn);
     
        request.query("SELECT ticker, openP, closeP, high, low　FROM mydb.dbo.stock_table").then(function (recordSet) {
            console.log(recordSet);
            dbConn.close();
        }).catch(function (err) {
            
            console.log(err);
            dbConn.close();
        });
    }).catch(function (err) {
        
        console.log(err);
    });
}

console.log("Connecting!!");

loadEmployees();