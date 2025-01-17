

$(document).ready(function () {
    // Add Item Functionality
    $('#add-item').click(function () {
        // Redirect to the add view 
        window.location.href = "/Department/Add";


    });


    $(document).ready(function () {
        // Handle Edit button click
        $(".edit-btn").on("click", function () {
            const departmentId = $(this).data("id");
            // Redirect to the Edit view with the department ID as a query parameter
            window.location.href = `/Department/Edit?id=${departmentId}`;
        });
    });
    // Cancel button - redirect to the department list page
    $('#dcancel-btn').click(function () {
        window.location.href = '/Department/Index'; // Adjust to your department list page
    });

    $(document).ready(function () {
        $(".delete-btn").on("click", function () {
            const id = $(this).data("id");
            if (confirm("Are you sure you want to delete this category?")) {
                $.ajax({
                    url: '/Department/Delete',
                    type: 'POST',
                    data: { id: id },
                    success: function () {
                        alert("Department deleted successfully!");
                        location.reload();
                    },
                    error: function (error) {
                        alert("An error occurred while deleting the Department.");

                    }
                });
            }
        });


    });
});

// Handle form submission via AJAX in Add department page
$(document).ready(function () {
    // Handle form submission via AJAX
    $('#add-department-form').submit(function (event) {
        event.preventDefault(); // Prevent the default form submission

        let departmentName = $('#departmentName').val();
        let status = $('input[name="status"]:checked').val();

        $.ajax({
            url: '/Department/Add', // Adjust the action URL if needed
            type: 'POST',
            data: { departmentName: departmentName, status: status },
            success: function (response) {
                // Redirect to the department list after successful submission
                window.location.href = '/Department/Index'; // Adjust this URL to your list page
            },
            error: function () {
                alert('Failed to add the department.');
            }
        });
    });

    // Cancel button - redirect to the department list page
    $('#cancel-btn').click(function () {
        window.location.href = '/Department/Index'; // Adjust to your department list page
    });
});

