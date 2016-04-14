$("#CityId").change(function () {
    $.ajax({
        url: app_root + "General/JsonDistricts",
        data: { cityId: $(this).val() },
        success: function (result) {
            $('#DistrictId').empty();
            $.each(result, function (index, district) {
                $('#DistrictId').append($('<option/>', {
                    value: district.Id,
                    text: district.Name
                }));
            }); // end each
        } // end success
    }); // ajax
}); // change