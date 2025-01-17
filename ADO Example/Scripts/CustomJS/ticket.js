$(document).ready(function () {


    $.ajax({
        url: '/Ticket/GetTickets',
        method: 'GET',
        success: function (data) {
            var tbody = $('#ticketGrid tbody');
            tbody.empty();

            data.forEach(function (ticket) {
                var statusClass = ticket.Status === "Resolved" ? "status-Resolved" : "status-open";
                var row = `
                    <tr>
                        <td>${ticket.ShowDate}</td>
                        <td>
                            <a href="#" class="ticket-icon" data-ticketno="${ticket.TicketNo}">
                                <i class="fas fa-ticket-alt"></i> ${ticket.TicketNo}
                            </a>
                        </td>
                        <td>${ticket.DepartmentName}</td>
                        <td>${ticket.CategoryName}</td>
                        <td>${ticket.SubCategoryName}</td>
                        <td>
                            <span class="${statusClass}">${ticket.Status}</span>
                        </td>
                    </tr>`;
                tbody.append(row);
            });

            // Attach click event to ticket numbers
            $(".ticket-icon").click(function (e) {
                e.preventDefault();
                var ticketNo = $(this).data("ticketno");

                // Fetch ticket details
                $.ajax({
                    url: '/Ticket/GetTicketDetails',
                    method: 'POST',
                    data: { ticketNo: ticketNo },
                    success: function (data) {
                        //console.log(response);
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
                        //console.error(xhr.responseText);
                        alert("Failed to load ticket details.");
                        console.error(error);
                    }
                });
            });
        },



        error: function (error) {
            console.error('Error fetching tickets:', error);
            alert('Failed to load ticket data.');
        }
    });
});

$(document).ready(function () {
    $("#getTicketDetails").click(function () {
        var ticketNo = $("#ticketNo").val();
        $.ajax({
            type: "POST",
            url: '@Url.Action("GetTicketDetails", "Ticket")',
            data: { ticketNo: ticketNo },
            success: function (data) {
                if (data.success) {
                    $("#ticketDetails").show();
                    // Populate the fields
                    $("strong:contains('Ticket No:')").next().text(data.ticket.TicketNo);
                    $("strong:contains('Created Date:')").next().text(data.ticket.CreateDate);
                    $("strong:contains('Department:')").next().text(data.ticket.DepartmentName);
                    $("strong:contains('Category:')").next().text(data.ticket.CategoryName);
                    $("strong:contains('SubCategory:')").next().text(data.ticket.SubCategoryName);
                    $("strong:contains('Status:')").next().text(data.ticket.Status);
                    $("strong:contains('Feedback:')").next().text(data.ticket.Feedback);
                } else {
                    alert(data.message);
                }
            }
        });
    });


    $(document).ready(function () {
        // Handling form submission with AJAX
        $("#updateTicketForm").submit(function (e) {
            e.preventDefault();

            var formData = $(this).serialize();

            $.ajax({
                type: "POST",
                url: '@Url.Action("UpdateTicket", "Ticket")',
                data: formData,
                success: function (response) {
                    if (response.success) {
                        alert(response.message);
                        window.location.href = '@Url.Action("Index", "Ticket")';
                    } else {
                        alert(response.message);
                    }
                },
                error: function () {
                    alert("An error aagya . Please try again.");
                }
            });
        });
    });

});