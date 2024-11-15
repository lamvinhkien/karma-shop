// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

// Pie Chart Example
var ctx = document.getElementById("myPieChart");

$.post('/dashboard/home/SalesCategory', { year: 2024 }, (d) => {
  $("#label-pie").html(d.map(p => `
      <span class="mr-2">
          <i class="fas fa-circle" style="color: ${p.backgroundColor};"></i> ${p.name}
      </span>
    `))

  let dat = {
    labels: [],
    datasets: [{
      data: [],
      backgroundColor: [],
      hoverBackgroundColor: [],
      hoverBorderColor: "rgba(234, 236, 244, 1)",
    }],
  }

  dat['labels'] = d.map(p => p.name)
  dat['datasets'][0]['data'] = d.map(c => c.sales)
  dat['datasets'][0]['backgroundColor'] = d.map(c => c.backgroundColor)
  dat['datasets'][0]['hoverBackgroundColor'] = d.map(c => c.hoverBackgroundColor)

  var myPieChart = new Chart(ctx, {
    type: 'doughnut',
    data: dat,
    options: {
      maintainAspectRatio: false,
      tooltips: {
        backgroundColor: "rgb(255,255,255)",
        bodyFontColor: "#858796",
        borderColor: '#dddfeb',
        borderWidth: 1,
        xPadding: 15,
        yPadding: 15,
        displayColors: false,
        caretPadding: 10,
      },
      legend: {
        display: false
      },
      cutoutPercentage: 80,
    },
  });
})

