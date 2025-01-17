$(document).ready(function () {
    loadUserMasterData();
    loadDepartments();

    // Redirect to Add User page when the 'Add Item' button is clicked
    $('#add-item').click(function () {
        window.location.href = "/UserMaster/Add";
    });

    //add post
    $('#addUserForm').submit(function (e) {
        e.preventDefault();

        var formData = {
            firstName: $('#firstName').val(),
            middleName: $('#middleName').val(),
            lastName: $('#lastName').val(),
            username: $('#username').val(),
            role: $('#role').val(),
            password: $('#password').val(),
            confirmPassword: $('#confirmPassword').val(),
            department: $('#department').val(),
            designation: $('#designation').val(),
            mobileNo: $('#mobileNo').val(),
            email: $('#email').val(),
            status: $("input[name='status']:checked").val()
        };

        // Validate password match
        if (formData.password !== formData.confirmPassword) {
            alert("Password and Confirm Password do not match!");
            return;
        }

        // Send the form data via AJAX
        $.ajax({
            url: '/UserMaster/AddUser',
            method: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert('User added successfully!');
                    window.location.href = '/UserMaster/Index'; // Redirect to the user list
                } else {
                    alert('Error: ' + response.message);
                }
            },
            error: function () {
                alert('Error occurred while adding the user.');
            }
        });
    });

    // edit user
    $(document).on('click', '.editBtn', function () {
        var userId = $(this).data('id');
        window.location.href = '/UserMaster/Edit/' + userId; // Redirect to edit page
    });
    $('#saveRoleChanges').click(function () {
        var updatedRole = {
            RoleID: $('#editRoleId').val(),
            RoleName: $('#editRoleName').val(),
            Status: $('#editStatus').val()
        };

        $.ajax({
            url: '/RoleMaster/UpdateRole', // Endpoint to update the role
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(updatedRole),
            success: function (response) {
                if (response.success) {
                    alert('Role updated successfully!');
                    window.location.href = '/RoleMaster/Index'; // Redirect to the grid view
                } else {
                    alert('Failed to update role. Please try again.');
                }
            },
            error: function () {
                alert('An error occurred while updating the role.');
            }
        });
    });
    

    // Delete user functionality
    $(document).on('click', '.deleteBtn', function () {
        var userId = $(this).data('id');

        if (confirm("Are you sure you want to delete this user?")) {
            $.ajax({
                url: '/UserMaster/DeleteUser', // Adjust the route if necessary
                type: 'POST',
                data: { userID: userId },
                success: function (response) {
                    if (response.success) {
                        alert('User deleted successfully!');
                        loadUserMasterData(); // Reload the user data
                    } else {
                        alert('Error deleting user');
                    }
                },
                error: function () {
                    alert('Error occurred while deleting the user');
                }
            });
        }
    });
});

// Load departments and populate the department select dropdown
function loadDepartments() {
    $.ajax({
        url: '/UserMaster/GetAllDepartments',
        method: 'GET',
        success: function (data) {
            var departmentSelect = $('#department');
            data.forEach(function (department) {
                departmentSelect.append(new Option(department.DepartmentName, department.ID));
            });
        }
    });
}

// Load user data and populate the user master table
function loadUserMasterData() {
    $.ajax({
        url: '/UserMaster/GetUserMasterData',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var userTable = $('#userMasterTable tbody');
            userTable.empty(); // Clear existing rows

            // Loop through the user data and append rows to the table
            $.each(data, function (index, user) {
                var fullName = user.FirstName + ' ' + user.LastName; // Combine first and last names
                userTable.append('<tr>' +
                    '<td>' + user.UserID + '</td>' +
                    '<td>' + fullName + '</td>' +
                    '<td>' + user.DepartmentName + '</td>' +
                    '<td>' + user.MobileNo + '</td>' +
                    '<td>' + user.Email + '</td>' +
                    '<td>' + user.Status + '</td>' +
                    '<td>' +
                    
                    '<button class="btn btn-warning editBtn" data-id="' + user.UserID + '"style="margin-right: 14px;"    >Edit</button>' +
                    '<button class="btn btn-danger deleteBtn" data-id="' + user.UserID + '">Delete</button>' +
                    '</td>' +
                    '</tr>');
            });
        },
        error: function (xhr, status, error) {
            console.log('Error: ' + error);
        }
    });
}

// Handle the form submission for editing a user
$('#editUserForm').submit(function (e) {
    e.preventDefault();

    var formData = $(this).serialize(); // Serialize the form data

    $.ajax({
        url: '@Url.Action("Edit", "UserMaster")',
        method: 'POST',
        data: formData,
        success: function (response) {
            if (response.success) {
                window.location.href = '/UserMaster/Index'; 
                //alert(response.message);
                
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert('Error updating user!');
        }
    });
});
