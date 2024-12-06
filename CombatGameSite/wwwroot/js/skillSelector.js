function updateCost() {
    let totalCost = 0;

    // Calculate cost of selected skills
    const selectedSkills = $('.skill-checkbox:checked');
    selectedSkills.each(function () {
        totalCost += parseInt($(this).data('cost')) || 0;
    });

    // Add health cost (assuming 1 point = 2 health)
    const healthCost = ($('#health').val() - 100) / 2;
    totalCost += parseInt(healthCost) || 0;

    // Update the total cost display
    $('#totalCost').text(totalCost);

    // Disable the submit button if total cost exceeds max points
    $('#submitButton').prop('disabled', totalCost > MAX_SKILL_POINTS);

    // Handle enabling/disabling checkboxes based on selection count
    const selectedCount = selectedSkills.length;
    $('.skill-checkbox').each(function () {
        if (!$(this).is(':checked')) {
            $(this).prop('disabled', selectedCount >= MAX_SKILL_SELECTIONS);
        }
    });
}

$(document).ready(function () {
    // Attach change event to skill checkboxes
    $('.skill-checkbox').on('change', updateCost);

    // Update the cost and checkbox states on page load
    updateCost();
});
