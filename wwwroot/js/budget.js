
function openEditModalBudget(budgetId, category, amount, startDate, endDate) {
    try {
        const editBudgetId = document.getElementById("editBudgetId");
        const editCategory = document.getElementById("editCategory");
        const editAmount = document.getElementById("editAmount");
        const editStartDate = document.getElementById("editStartDate");
        const editEndDate = document.getElementById("editEndDate");
        const editModal = document.getElementById("editModal-budget");

        // Check if all elements exist before assigning values
        if (!editBudgetId || !editCategory || !editAmount || !editStartDate || !editEndDate || !editModal) {
            console.error("One or more elements are missing in the DOM.");
            return;
        }

        editBudgetId.value = budgetId;
        editCategory.value = category;
        editAmount.value = amount;
        editStartDate.value = startDate;
        editEndDate.value = endDate;

        editModal.style.display = "block";
    } catch (error) {
        console.error("Error in openEditModalBudget:", error);
    }
}

function closeModal() {
    const editModal = document.getElementById("editModal-budget");
    if (editModal) {
        editModal.style.display = "none";
    } else {
        console.error("editModal-budget element not found.");
    }
}

function openDeleteConfirmation_budget(budgetId) {
    if (confirm("Are you sure you want to delete this budget?")) {
        fetch(`/Budgets/DeleteBudget/${budgetId}`, {
            method: "DELETE"
        })
            .then(response => {
                if (response.ok) {
                    alert("Budget deleted successfully.");
                    location.reload();
                } else {
                    alert("Failed to delete the budget.");
                }
            })
            .catch(error => console.error("Error in delete fetch request:", error));
    }
}
