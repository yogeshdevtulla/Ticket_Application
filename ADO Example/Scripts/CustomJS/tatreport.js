$(document).ready(function () {
    $.ajax({
        url: '/TATReport/GetTATReportData',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var rows = '';
            data.forEach(function (item) {
                rows += `
                        <tr>
                            <td>${item.ShowDate}</td>
                            <td>${item.CloseDate}</td>
                            <td>${item.TicketNumber}</td>
                            <td>${item.Department}</td>
                            <td>${item.Category}</td>
                            <td>${item.Subcategory}</td>
                            <td>${item.Status}</td>
                            <td>${item.TATDate}</td> <!-- TAT is already formatted -->
                        </tr>
                    `;
            });
            $('#tatReportGrid tbody').html(rows);
        },
        error: function (err) {
            alert('Error fetching data.');
        }
    });
});