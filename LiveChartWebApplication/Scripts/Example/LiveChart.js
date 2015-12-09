var LiveChartVM = (function () {
   return {
      // On data update, update the chart.
      updateChart: function (iItem, iElement) {
         var vm = this;
         var data = vm.Data();

         if (vm._chart == null) {
            vm._chart = this.createChart(data, iElement);
            vm._counter = data.length;
         }
         else {
            for (var i = 0; i < data.length; i++) {
               vm._chart.addData([data[i]], vm._counter++);
               // Remove the oldest data.
               vm._chart.removeData();
            }
         }
         // Reset the data.
         vm.Data(null); 
      },

      // Create the chart with ChartJS.
      createChart: function (iData, iElement) {
         var labels = [];
         for (var i = 0 ; i < iData.length; i++)
            labels.push(i);

         var chartData = {
            labels: labels,
            datasets: [{
               label: "My live dataset",
               data: iData,
               fillColor: "rgba(151,187,205,0.2)",
               strokeColor: "rgba(151,187,205,1)",
               pointColor: "rgba(151,187,205,1)",
               pointStrokeColor: "#fff",
               pointHighlightFill: "#fff",
               pointHighlightStroke: "rgba(151,187,205,1)"
            }]
         };

         return new Chart(iElement.getContext('2d')).Line(chartData, { responsive: true, animation: false });
      }
   }
})();