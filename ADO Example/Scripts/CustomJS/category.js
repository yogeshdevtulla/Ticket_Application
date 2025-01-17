$(document).ready(function () {
    // Add Item Functionality
    $('#add-item').click(function () {
        // Redirect to the add view 
        window.location.href = "/Category/Add";

    });
});

    // Handle form submission via AJAX in Add department page
$(document).ready(function () {
    // Handle form submission via AJAX
    $('#add-category-form').submit(function (event) {
        event.preventDefault(); // Prevent the default form submission

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
    $('#cancel-btn').click(function () {
        window.location.href = '/Department/Index'; // Adjust to your department list page
    });
});
  
    // Edit Category via AJAX
    /*$(".edit-btn").click(function () {
        var categoryId = $(this).data("id");
        var categoryName = $(this).data("name");
        var departmentName = $(this).data("department");
        var status = $(this).data("status");

        var newCategoryName = prompt("Edit Category Name:", categoryName);
        var newDepartmentName = prompt("Edit Department Name:", departmentName);
        var newStatus = prompt("Edit Status:", status);

        if (newCategoryName && newDepartmentName && newStatus) {
            var token = $("input[name='__RequestVerificationToken']").val();  // CSRF Token

            $.ajax({
                url: "/Category/Edit",
                type: "POST",
                data: {
                    __RequestVerificationToken: token,  // Including the token in the request data
                    categoryId: categoryId,
                    categoryName: newCategoryName,
                    departmentName: newDepartmentName,
                    status: newStatus
                },
                success: function (response) {
                    if (response.success) {
                        alert(response.message);
                        location.reload();  // Reload the page to show updated data
                    } else {
                        alert(response.message);
                    }
                },
                error: function () {
                    alert("An error occurred while updating the category.");
                }
            });
        } else {
            alert("All fields are required!");
        }
    });*/


//$(document).ready(function () {

//    $('.delete-btn').click(function () {
//        let row = $(this).closest('tr');
//        let id = row.data('id');

//        if (confirm("Are you sure you want to delete this record?")) {
//            $.post('/Category/Delete', { id }, function () {
//                row.remove();
//            });
//        }
//    });

//});

$(document).ready(function () {
    $(".delete-btn").on("click", function () {
        const CategoryID = $(this).data("id");
        if (confirm("Are you sure you want to delete this category?")) {
            $.ajax({
                url: '/Category/Delete',
                type: 'POST',
                data: { CategoryID: CategoryID },
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

