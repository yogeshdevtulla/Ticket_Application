$(document).ready(function () {
    // Event listener for Open Tickets card
    $("#openTicketsCard").on("click", function () {
        // Fetch open ticket numbers from the server
        fetch("/Dashboard/GetOpenTickets")
            .then(response => response.json())
            .then(data => {
                console.log(data); // Log the response to check its structure
                const ticketList = $("#openTicketsList");
                ticketList.empty();

                if (data && data.length > 0) {
                    data.forEach(ticket => {
                        
                        ticketList.append(`<li class="list-group-item"><a href="#" class="ticket-icon" data-ticketno="${ticket.TicketNo}">
                            <i class="fas fa-ticket-alt"></i> ${ticket.TicketNo}
                        </a></li>`);
                    });
                } else {
                    ticketList.append('<li class="list-group-item">No open tickets found.</li>');
                }

                $("#openTicketsModal").modal("show");
            })
            .catch(error => console.error("Error fetching open tickets:", error));


    });
    // Use event delegation to handle click events for dynamically added .ticket-icon elements
    $(document).on("click", ".ticket-icon", function (e) {
        e.preventDefault();
        var ticketNo = $(this).data("ticketno");
        $(".modal").modal("hide");

        // Fetch ticket details
        $.ajax({
            url: '/Ticket/GetTicketDetails',
            method: 'POST',
            data: { ticketNo: ticketNo },
            success: function (data) {
                if (data.success) {
                    // Populate modal fields
                    $("#modalTicketNo").text(data.ticket.TicketNo);
                    $("#modalTicketDate").text(data.ticket.ShowDate);
                    $("#modalIssuedBy").text(data.ticket.IssuedBy || "N/A");
                    $("#modalDepartment").text(data.ticket.DepartmentName);
                    $("#modalCategory").text(data.ticket.CategoryName);
                    $("#modalSubCategory").text(data.ticket.SubCategoryName);
                    $("#modalStatus").text(data.ticket.Status);
                    $("#modalFeedback").text(data.ticket.Feedback);

                    // Show modal
                    $("#viewTicketModal").modal("show");
                } else {
                    alert(data.message);
                }
            },
            error: function (error) {
                alert("Failed to load ticket details.");
                console.error(error);
            }
        });
    });

    

});

fetch('/Dashboard/PieChart')
            .then(response => response.json())
            .then(data => {
                const labels = data.map(d => d.DepartmentName);
                const values = data.map(d => d.TicketCount);

                const pieChart = new Chart(document.getElementById('pieChart'), {
                    type: 'pie',
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Tickets by Department',
                            data: values,
                            backgroundColor: [
                                'rgba(255, 99, 132, 0.6)',
                                'rgba(54, 162, 235, 0.6)',
                                'rgba(255, 206, 86, 0.6)',
                                'rgba(75, 192, 192, 0.6)',
                                'rgba(153, 102, 255, 0.6)',
                                'rgba(255, 159, 64, 0.6)'
                            ],
                            borderWidth: 1
                        }]
                    }
                });
            });
            
        // Bar Graph Data
        fetch('/Dashboard/BarGraph')
            .then(response => response.json())
            .then(data => {
                const labels = data.map(d => new Date(d.TicketDate).toLocaleDateString());
                const openTickets = data.map(d => d.OpenTickets);
                const closedTickets = data.map(d => d.ClosedTickets);
                const resolvedTickets = data.map(d => d.ResolvedTickets);

                const barGraph = new Chart(document.getElementById('barGraph'), {
                    type: 'bar',
                    data: {
                        labels: labels,
                        datasets: [
                            {
                                label: 'Open Tickets',
                                data: openTickets,
                                backgroundColor: 'rgba(54, 162, 235, 0.6)',
                                borderWidth: 1
                            },
                            {
                                label: 'Closed Tickets',
                                data: closedTickets,
                                backgroundColor: 'rgba(255, 99, 132, 0.6)',
                                borderWidth: 1
                            },
                            {
                                label: 'Resolved Tickets',
                                data: resolvedTickets,
                                backgroundColor: 'rgba(75, 192, 192, 0.6)',
                                borderWidth: 1
                            }
                        ]
                    },
                    options: {
                        scales: {
                            x: {
                                beginAtZero: true
                            },
                            y: {
                                beginAtZero: true
                            }
                        }
                    }
                });
            });


 