$(function () { 
				axios.get('http://localhost:9000/api/historicalRate/BRL')
				  .then(function (response) {
					Highcharts.stockChart('usdToBrl', {
						rangeSelector: {
							selected: 1
						},

						title: {
							text: 'USD Conversion To BRL'
						},

						series: [{
							name: 'Conversion',
							data: response.data.Quotes,
							tooltip: {
								valueDecimals: 2
							}
						}]
									
				  });
				  })
				  .catch(function (error) {
					console.log(error);
				  });
				  
				axios.get('http://localhost:9000/api/historicalRate/EUR')
				  .then(function (response) {
					Highcharts.stockChart('usdToEur', {
						rangeSelector: {
							selected: 1
						},

						title: {
							text: 'USD Conversion To EUR'
						},

						series: [{
							name: 'Conversion',
							data: response.data.Quotes,
							tooltip: {
								valueDecimals: 2
							}
						}]
									
				  });
				  })
				  .catch(function (error) {
					console.log(error);
				  });	
				  
				axios.get('http://localhost:9000/api/historicalRate/ARS')
				  .then(function (response) {
					Highcharts.stockChart('usdToArs', {
						rangeSelector: {
							selected: 1
						},

						title: {
							text: 'USD Conversion To ARS'
						},

						series: [{
							name: 'Conversion',
							data: response.data.Quotes,
							tooltip: {
								valueDecimals: 2
							}
						}]
									
				  });
				  })
				  .catch(function (error) {
					console.log(error);
				  });	
			});
			
