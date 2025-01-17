$(document).ready(function () {
    loadRoles();
    //add item
    $('#addRoleBtn').click(function () {
        // Redirect to the "Add User" page
        window.location.href = "/RoleMaster/Add";
    });

    $('#addRoleForm').submit(function (e) {
        e.preventDefault();

        // Get form data
        var formData = {
            rollname: $('#rollname').val(),
            status: $("input[name='status']:checked").val()
        };

        
        // Send the data via AJAX
        $.ajax({
            url: '/RoleMaster/AddRole', // Your controller action
            method: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert('User added successfully!');
                    window.location.href = '/RoleMaster/Index'; // Redirect after success
                } else {
                    alert('Error: ' + response.message);
                }
            },
            error: function () {
                alert('Error occurred while adding the user.');
            }
        });
    });

    $(document).on('click', '.editBtn', function () {
        var roleId = $(this).data('id'); // Get the RoleID from the button's data-id attribute
        if (roleId) {
            window.location.href = '/RoleMaster/Edit/' + roleId; // Redirect to the edit page
        } else {
            alert('Role ID is missing. Cannot edit this role.');
        }
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


    //delete
    $(document).on('click', '.deleteBtn', function () {
        var roleId = $(this).data('id');
        if (confirm('Are you sure you want to delete this role?')) {
            $.ajax({
                url: '/RoleMaster/DeleteRole',
                type: 'POST',
                data: { id: roleId },
                success: function () {
                    alert('Role deleted successfully!');
                    loadRoles();
                },
                error: function () {
                    alert('An error occurred while deleting the role.');
                }
            });
        }
    });

});
function loadRoles() {
    $.ajax({
        url: '/RoleMaster/GetRoles',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var RoleTable = $('#roleTable tbody');
            RoleTable.empty()
            // Loop through the data and append rows to the table
            $.each(data, function (index, role) {
                var status = role.Status === 'Active' ? 'Active' : 'Inactive'; // Map status to display text
                $('#roleTable tbody').append('<tr>' +
                    '<td>' + role.RoleID + '</td>' +
                    '<td>' + role.RoleName + '</td>' +
                    '<td>' + status + '</td>' +
                    '<td>' +
                    //'<button class="btn btn-warning editBtn" data-id="${role.RoleID}">Edit</button>' +
                    '<button class="btn btn-warning editBtn" data-id="' + role.RoleID + '"style="margin-right: 14px;">Edit</button>'+
                    //'<a href="/RoleMaster/Edit/${role.RoleID}" class="btn btn-warning">Edit</a>' +
                    //'<button class="btn btn-warning editBtn" data-id="' + role.RoleID + '">Edit</button> ' +
                    '<button class="btn btn-danger deleteBtn" data-id="' + role.RoleID + '">Delete</button>' +
                    '</td>' +
                    '</tr>');
            });
            //$('#roleTable tbody').html(rows);

        }



    });
}


