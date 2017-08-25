//This is a sample for generate a csv file by parsing a json string in Node.js 
var json2csv= require('json2csv');
var fs = require('fs');

var json2 ={"data":[{"date":"2017-07-27","open":13.15,"high":14.06,"low":13.14,"close":13.91,"volume":21605359.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":13.15,"adj_high":14.06,"adj_low":13.14,"adj_close":13.91,"adj_volume":21605359.0},{"date":"2017-07-26","open":13.74,"high":13.82,"low":13.4,"close":13.4,"volume":20962854.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":13.74,"adj_high":13.82,"adj_low":13.4,"adj_close":13.4,"adj_volume":20962854.0},{"date":"2017-07-25","open":14.06,"high":14.11,"low":13.65,"close":13.89,"volume":15148893.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":14.06,"adj_high":14.11,"adj_low":13.65,"adj_close":13.89,"adj_volume":15148893.0},{"date":"2017-07-24","open":14.45,"high":14.48,"low":14.02,"close":14.08,"volume":14750811.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":14.45,"adj_high":14.48,"adj_low":14.02,"adj_close":14.08,"adj_volume":14750811.0},{"date":"2017-07-21","open":14.8,"high":14.84,"low":14.34,"close":14.48,"volume":14252724.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":14.8,"adj_high":14.84,"adj_low":14.34,"adj_close":14.48,"adj_volume":14252724.0},{"date":"2017-07-20","open":15.05,"high":15.14,"low":14.72,"close":14.89,"volume":11848949.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":15.05,"adj_high":15.14,"adj_low":14.72,"adj_close":14.89,"adj_volume":11848949.0},{"date":"2017-07-19","open":14.86,"high":15.24,"low":14.8,"close":14.97,"volume":14394582.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":14.86,"adj_high":15.24,"adj_low":14.8,"adj_close":14.97,"adj_volume":14394582.0},{"date":"2017-07-18","open":15.17,"high":15.25,"low":14.63,"close":14.73,"volume":19933981.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":15.17,"adj_high":15.25,"adj_low":14.63,"adj_close":14.73,"adj_volume":19933981.0},{"date":"2017-07-17","open":15.3,"high":15.43,"low":15.12,"close":15.13,"volume":13272511.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":15.3,"adj_high":15.43,"adj_low":15.12,"adj_close":15.13,"adj_volume":13272511.0},{"date":"2017-07-14","open":15.44,"high":15.59,"low":15.265,"close":15.27,"volume":14634279.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":15.44,"adj_high":15.59,"adj_low":15.265,"adj_close":15.27,"adj_volume":14634279.0},{"date":"2017-07-13","open":15.78,"high":15.97,"low":15.56,"close":15.69,"volume":21598350.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":15.78,"adj_high":15.97,"adj_low":15.56,"adj_close":15.69,"adj_volume":21598350.0},{"date":"2017-07-12","open":15.54,"high":15.75,"low":15.21,"close":15.24,"volume":19528366.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":15.54,"adj_high":15.75,"adj_low":15.21,"adj_close":15.24,"adj_volume":19528366.0},{"date":"2017-07-11","open":16.33,"high":16.36,"low":15.44,"close":15.47,"volume":41893429.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":16.33,"adj_high":16.36,"adj_low":15.44,"adj_close":15.47,"adj_volume":41893429.0},{"date":"2017-07-10","open":17.16,"high":17.21,"low":16.95,"close":16.99,"volume":8566640.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":17.16,"adj_high":17.21,"adj_low":16.95,"adj_close":16.99,"adj_volume":8566640.0},{"date":"2017-07-07","open":17.33,"high":17.38,"low":17.15,"close":17.18,"volume":4546654.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":17.33,"adj_high":17.38,"adj_low":17.15,"adj_close":17.18,"adj_volume":4546654.0},{"date":"2017-07-06","open":17.25,"high":17.38,"low":17.235,"close":17.31,"volume":6191798.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":17.25,"adj_high":17.38,"adj_low":17.235,"adj_close":17.31,"adj_volume":6191798.0},{"date":"2017-07-05","open":17.57,"high":17.59,"low":17.22,"close":17.32,"volume":6042612.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":17.57,"adj_high":17.59,"adj_low":17.22,"adj_close":17.32,"adj_volume":6042612.0},{"date":"2017-07-03","open":17.91,"high":17.92,"low":17.45,"close":17.59,"volume":3285653.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":17.91,"adj_high":17.92,"adj_low":17.45,"adj_close":17.59,"adj_volume":3285653.0},{"date":"2017-06-30","open":18.03,"high":18.0799,"low":17.62,"close":17.77,"volume":5986804.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":18.03,"adj_high":18.0799,"adj_low":17.62,"adj_close":17.77,"adj_volume":5986804.0},{"date":"2017-06-29","open":17.69,"high":18.13,"low":17.67,"close":17.89,"volume":12193935.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":17.69,"adj_high":18.13,"adj_low":17.67,"adj_close":17.89,"adj_volume":12193935.0},{"date":"2017-06-28","open":17.34,"high":17.78,"low":17.33,"close":17.76,"volume":10455313.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":17.34,"adj_high":17.78,"adj_low":17.33,"adj_close":17.76,"adj_volume":10455313.0},{"date":"2017-06-27","open":17.21,"high":17.48,"low":17.2,"close":17.34,"volume":7718562.0,"ex_dividend":0.0,"split_ratio":1.0,"adj_open":17.21,"adj_high":17.48,"adj_low":17.2,"adj_close":17.34,"adj_volume":7718562.0}],"result_count":22,"page_size":100,"current_page":1,"total_pages":1,"api_call_credits":1};

var json3=
	[
	{
		"date":"2017-07-27",
		"open":13.15,
		"high":14.06,
		"low":13.14,
		"close":13.91,
		"volume":21605359.0,
		"ex_dividend":0.0,
		"split_ratio":1.0,
		"adj_open":13.15,
		"adj_high":14.06,
		"adj_low":13.14,
		"adj_close":13.91,
		"adj_volume":21605359.0
	},  {
		"date":"2017-07-26",
		"open":13.74,
		"high":13.82,
		"low":13.4,
		"close":13.4,
		"volume":20962854.0,
		"ex_dividend":0.0,
		"split_ratio":1.0,
		"adj_open":13.74,
		"adj_high":13.82,
		"adj_low":13.4,
		"adj_close":13.4,
		"adj_volume":20962854.0
	}, {
		"date":"2017-07-25",
		"open":14.06,
		"high":14.11,
		"low":13.65,
		"close":13.89,
		"volume":15148893.0,
		"ex_dividend":0.0,
		"split_ratio":1.0,
		"adj_open":14.06,
		"adj_high":14.11,
		"adj_low":13.65,
		"adj_close":13.89,
		"adj_volume":15148893.0
	}
	
	];

var json = {
		"stock_data":[
	{
		"date":"2017-07-27",
		"open":13.15,
		"high":14.06,
		"low":13.14
	}
	]
};

var json4 = [
  {
    "car": "Audi",
    "price": 40000,
    "color": "blue"
  }, {
    "car": "BMW",
    "price": 35000,
    "color": "black"
  }, {
    "car": "Porsche",
    "price": 60000,
    "color": "green"
  }
];
console.log(json4);

json2csv({data: json3, fields:['date',
							'open',
							'high',
							'low',
							'close', 
							'volume', 
							'ex_dividend',
							 'split_ratio',
							 'adj_open',
							 'adj_high',
							 'adj_low', 
							 'adj_close',
							 'adj_volume'
							 ]}, function(err, csv){
	if(err) console.log(err);
	fs.writeFile('stock6.csv', csv, function(err){
		if(err) throw err;
		console.log("Saved");
	});

});
