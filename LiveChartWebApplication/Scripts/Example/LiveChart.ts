class LiveChartVM {

   // On data update, update the chart.
   updateChart(iItem, iElement) {
      var vm: any = this;

      var data = vm.Data();
      if (data == null)
         return;

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
   }

   // Create the chart with ChartJS.
   createChart(iData, iElement) {
      var labels = [];
      for (var i = 0; i < iData.length; i++)
         labels.push(i);

      var chartData = {
         labels: labels,
         datasets: [{
            label: "My live dataset",
            data: iData,
            fillColor: "rgba(217,237,245,0.2)",
            strokeColor: "#9acfea",
            pointColor: "#9acfea",
            pointStrokeColor: "#fff"
         }]
      };

      return new Chart(iElement.getContext('2d')).Line(chartData, { responsive: true, animation: false });
   }
}