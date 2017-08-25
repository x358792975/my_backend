
//This is a sample for reading a csv file in Node.js
var fs = require('fs'); 
var parse = require('csv-parse');

var inputFile='sample.csv';
var csvData=[];
var rowData=[];

function split(dataStr){
	var newStr = str.split(",");
	rowData.push(dataStr);
}

fs.createReadStream(inputFile)
    .pipe(parse({delimiter: ':'}))
    .on('data', function(csvrow) {
        console.log(csvrow);
        //do something with csvrow
        split(csvrow)
        csvData.push(csvrow);        
    })
    .on('end',function() {
      //do something wiht csvData
      console.log("Done");
      console.log(csvData[0]);
      console.log(csvData[1]);
    });