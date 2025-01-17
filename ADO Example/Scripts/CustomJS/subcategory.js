$(document).ready(function () {
    // Add Item Functionality
    $('#add-item').click(function () {
        // Redirect to the add view 
        window.location.href = "/SubCategory/Add";

    });
});

// Handle form submission via AJAX in Add department page
$(document).ready(function () {
    // Handle form submission via AJAX
    $('#add-category-form').submit(function (event) {
        event.preventDefault(); 
        let categoryName = $('#categoryName').val();
        let departmentName = $('#departmentName').val();
        let status = $('input[name="status"]:checked').val();

        $.ajax({
            url: '/Category/Add', // Adjust the action URL if needed
            type: 'POST',
            data: { categoryName: categoryName, departmentName: departmentName, status: status },
            success: function (response) {
                // Redirect to the department list after successful submission
                window.location.href = '/Category/Index'; // Adjust this URL to your list page
            },
            error: function () {
                alert('Failed to add the department.');
            }
        });
    });
});
//delete

$(document).ready(function () {
    $(".delete-btn").on("click", function () {
        const SubCategoryID = $(this).data("id");
        if (confirm("Are you sure you want to delete this subcategory?")) {
            $.ajax({
                url: '/SubCategory/Delete',
                type: 'POST',
                data: { SubCategoryID: SubCategoryID },
                success: function () {
                    alert("category deleted successfully!");
                    location.reload();
                },
                error: function (error) {
                    alert("An error occurred while deleting the category.");

                }
            });
        }
    });
});
