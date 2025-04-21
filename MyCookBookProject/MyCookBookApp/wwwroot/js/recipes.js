document.addEventListener("DOMContentLoaded", function () {
  const BASE_URL = `${window.location.origin}/Recipe`; // Dynamically set base URL
  let selectedCategories = [];
  let selectedEditCategories = [];

  // Define a mapping of category numbers to their text values
  const categoryMapping = {
      0: "Breakfast",
      1: "Lunch",
      2: "Dinner",
      3: "Dessert",
      4: "Snack",
      5: "Vegan",
      6: "Vegetarian",
      7: "GlutenFree",
      8: "Keto",
      9: "LowCarb",
      10: "HighProtein"
  };

  fetchRecipes();

  // Handle category selection
  document.querySelectorAll("#categoryList .dropdown-item").forEach(item => {
      item.addEventListener("click", function (event) {
          event.preventDefault();
          let categoryValue = parseInt(this.getAttribute("data-value"), 10);

          if (selectedCategories.includes(categoryValue)) {
              // If already selected, remove it
              selectedCategories = selectedCategories.filter(c => c !== categoryValue);
          } else {
              // Otherwise, add it
              selectedCategories.push(categoryValue);
          }

          // Update button text to display selected categories
          document.getElementById("categoryDropdown").innerText =
              selectedCategories.length > 0 
                  ? selectedCategories.map(id => document.querySelector(`[data-value="${id}"]`).innerText).join(", ") 
                  : "Select Categories";

          // Update hidden input with selected categories
          document.getElementById("categories").value = JSON.stringify(selectedCategories);
          console.log("Selected Categories (Numeric):", selectedCategories);
      });
  });

  // Open the Add Recipe Modal
  document.getElementById("addRecipeBtn")?.addEventListener("click", function () {
      document.getElementById("addRecipeForm").reset();
      $('#addRecipeModal').modal('show');
  });

  // Modify Save Recipe function
  document.getElementById("saveRecipe").addEventListener("click", function (event) {
    event.preventDefault(); // Ensure the form doesn't submit automatically
    
    let recipe = {
        recipeId: "NewRecipe_Only",
        name: document.getElementById("Name").value.trim(),
        tagLine: document.getElementById("TagLine").value.trim(),
        summary: document.getElementById("Summary").value.trim(),
        ingredients: document.getElementById("ingredients").value.split(",").map(i => i.trim()),
        instructions: document.getElementById("instructions").value.split("\n").map(i => i.trim()),
        categories: JSON.parse(document.getElementById("categoryList").value || "[]"),
        media: []
    };
    
    // Validate required fields
    if (!recipe.name) {
        alert("Recipe name is required!");
        return;
    }
    
    console.log("Saving recipe:", JSON.stringify(recipe, null, 2));

    // Send recipe data to API
    fetch(`${BASE_URL}/Add`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(recipe)
    })
    .then(response => {
        if (!response.ok) {
            return response.text().then(text => {
                throw new Error(`Error ${response.status}: ${text}`);
            });
        }
        return response.json();
    })
    .then(data => {
        if (data.success) {
            console.log("Recipe added successfully!");
            // Close the modal properly
            const addModal = bootstrap.Modal.getInstance(document.getElementById('addRecipeModal'));
            addModal.hide();
            // Refresh the page after a slight delay
            setTimeout(() => {
                location.reload();
            }, 500);
        } else {
            alert("Failed to add recipe: " + (data.message || "Unknown error"));
        }
    })
    .catch(error => {
        console.error("Error adding recipe:", error);
        alert("Failed to add recipe: " + error.message);
    });
  });

  // Fetch all recipes
  function fetchRecipes() {
      console.log("Fetching all recipes...");
      fetch(`${BASE_URL}/GetAll`)
          .then(response => response.json())
          .then(recipes => {
              console.log("Recipes loaded:", recipes);
              displayRecipes(recipes);
          })
          .catch(error => console.error("Fetch error:", error));
  }

// Edit Recipe - Fetch Recipe Details and Show Modal
  window.editRecipe = function (recipeId) {
    fetch(`${BASE_URL}/${recipeId}`)  // Use backticks instead of double quotes
        .then(response => response.json())
        .then(recipe => {
            if (!recipe) {
                console.error("Recipe not found");
                return;
            }
            
            // Clear previous selections
            selectedEditCategories = [];
            
            // Debug: Print the full recipe object
            console.log("Loaded Recipe Object:", JSON.stringify(recipe, null, 2));
            
            // Assign recipe values to form fields
            document.getElementById("editRecipeId").value = recipe.recipeId;
            document.getElementById("editRecipeName").value = recipe.name;
            document.getElementById("editTagLine").value = recipe.tagLine || "";
            document.getElementById("editSummary").value = recipe.summary || "";
            document.getElementById("editIngredients").value =
                Array.isArray(recipe.ingredients) ? recipe.ingredients.join(", ") : "";
            document.getElementById("editInstructions").value =
                Array.isArray(recipe.instructions) ? recipe.instructions.join("\n") : "";
            
            // Handle categories
            const recipeCategories = Array.isArray(recipe.categories) ? recipe.categories : [];
            selectedEditCategories = [...recipeCategories]; // Copy the array to selectedEditCategories
            
            // Update UI for selected categories
            document.querySelectorAll("#editCategoryList .dropdown-item").forEach(option => {
                const categoryValue = parseInt(option.getAttribute("data-value"), 10);
                if (recipeCategories.includes(categoryValue)) {
                    option.classList.add("active");
                } else {
                    option.classList.remove("active");
                }
            });
            
            // Update dropdown button text
            const categoryNames = recipeCategories.map(catId => categoryMapping[catId] || "Unknown");
            document.getElementById("editCategoryDropdown").innerText =
                categoryNames.length > 0 ? categoryNames.join(", ") : "Select Categories";
            
            // Store selected categories in hidden input field
            document.getElementById("editCategories").value = JSON.stringify(recipeCategories);
            
            // Show the modal
            const editRecipeModal = new bootstrap.Modal(document.getElementById('editRecipeModal'));
            editRecipeModal.show();
        })
        .catch(error => console.error("Error fetching recipe:", error));
  };

  // Update Recipe function
  window.updateRecipe = function () {
      console.log("Update button clicked!");
      
      let recipe = {
          recipeId: document.getElementById("editRecipeId").value.trim(),
          name: document.getElementById("editRecipeName").value.trim(),
          tagLine: document.getElementById("editTagLine").value.trim(),
          summary: document.getElementById("editSummary").value.trim(),
          ingredients: document.getElementById("editIngredients")
              .value.split(",")
              .map(i => i.trim()),
          instructions: document.getElementById("editInstructions")
              .value.split("\n")
              .map(i => i.trim()),
          categories: JSON.parse(
              document.getElementById("editCategories").value || "[]"
          ),
          media: []
      };
      
      console.log("Updated Recipe Object:", JSON.stringify(recipe, null, 2));
      
      // Verify recipeId exists
      const recipeId = recipe.recipeId;
      if (!recipeId) {
          console.error("No recipe ID found!");
          alert("Recipe ID is missing. Cannot update.");
          return;
      }
      
      fetch(`${BASE_URL}/Edit/${recipeId}`, {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(recipe)
      })
      .then(response => {
          console.log("Response Status:", response.status);
          if (!response.ok) {
              return response.text().then(text => {
                  throw new Error(`Error ${response.status}: ${text}`);
              });
          }
          return response.json();
      })
      .then(data => {
          console.log("Recipe updated successfully:", data);
          alert("Recipe updated successfully!");
          
          // Close the modal and refresh
          let modal = bootstrap.Modal.getInstance(
              document.getElementById("editRecipeModal")
          );
          modal.hide();
          
          setTimeout(() => {
              location.reload();
          }, 500);
      })
      .catch(error => {
          console.error("Error updating recipe:", error);
          alert("Failed to update recipe: " + error.message);
      });
  };

    // Handle Update Recipe event
  document.getElementById("updateRecipe").addEventListener("click", function (event) {
    event.preventDefault(); // Prevent form from reloading the page
    updateRecipe(); // Call the update function
  });
  // Delete Recipe
  window.deleteRecipe = function (recipeId) {
      if (!confirm("Are you sure you want to delete this recipe?")) return;
      fetch(`${BASE_URL}/Delete/${recipeId}`, {
          method: "DELETE"
      })
      .then(response => response.json())
      .then(data => {
          if (data.success) {
              fetchRecipes();
          } else {
              alert("Failed to delete recipe: " + data.message);
          }
      })
      .catch(error => console.error("Error deleting recipe:", error));
  };

  // Search for recipes
  window.searchRecipes = function () {
      let keyword = document.getElementById("searchInput").value.trim();
      if (!keyword) {
          fetchRecipes();
          return;
      }
      let searchRequest = { keyword: keyword, categories: [] };
      fetch(`${BASE_URL}/Search`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(searchRequest)
      })
      .then(response => response.json())
      .then(recipes => {
          displayRecipes(recipes);
      })
      .catch(error => console.error("Search error:", error));
  };

  // Add event listeners to all dropdown items in the edit category list
  document.querySelectorAll("#editCategoryList .dropdown-item").forEach(item => {
      item.addEventListener("click", function (event) {
          event.preventDefault();
          let categoryValue = parseInt(this.getAttribute("data-value"), 10);
          
          if (selectedEditCategories.includes(categoryValue)) {
              // If already selected, remove it
              selectedEditCategories = selectedEditCategories.filter(c => c !== categoryValue);
              this.classList.remove("active");
          } else {
              // Otherwise, add it
              selectedEditCategories.push(categoryValue);
              this.classList.add("active");
          }
          
          // Update button text to display selected categories
          document.getElementById("editCategoryDropdown").innerText =
              selectedEditCategories.length > 0 
                  ? selectedEditCategories.map(id => 
                      document.querySelector(`#editCategoryList [data-value="${id}"]`).innerText).join(", ")
                  : "Select Categories";
          
          // Update hidden input with selected categories
          document.getElementById("editCategories").value = JSON.stringify(selectedEditCategories);
          
          console.log("Selected Edit Categories (Numeric):", selectedEditCategories);
      });
  });

  // Handle Enter key press for search
  window.handleEnter = function (event) {
      if (event.key === "Enter") {
          searchRecipes();
      }
  };

  // Show or hide the clear search button
  window.toggleClearButton = function () {
      let searchInput = document.getElementById("searchInput");
      let clearButton = document.getElementById("clearSearch");
      clearButton.style.display = searchInput.value ? "block" : "none";
  };

  // Clear search and reload all recipes
  window.clearSearch = function () {
      document.getElementById("searchInput").value = "";
      document.getElementById("clearSearch").style.display = "none";
      fetchRecipes();
  };

  // Display recipes dynamically
  function displayRecipes(recipes) {
      let cardContainer = document.getElementById("recipeCards");
      cardContainer.innerHTML = "";
      
      if (!recipes || recipes.length === 0) {
          cardContainer.innerHTML = '<div class="alert alert-info mt-3">No recipes found.</div>';
          return;
      }
      
      recipes.forEach(recipe => {
          let imageUrl = recipe.media && recipe.media.length > 0 ? recipe.media[0].url : "placeholder.jpg";
          // Convert category numbers to text labels
          let categoriesText = (recipe.categories || []).map(catId =>
              categoryMapping[catId] || "Unknown").join(" | ");
              
          let card = `
              <div class="mb-3">
                  <div class="card shadow-sm">
                      <div class="card-body">
                          <div class="d-flex justify-content-between align-items-center">
                              <div>
                                  <h5 class="card-title mb-0">${recipe.name}</h5>
                                  <p class="text-muted mb-2">${recipe.tagLine || ""}</p>
                              </div>
                              <div>
                                  <button class="btn btn-warning btn-sm me-2"
                                      onclick="editRecipe('${recipe.recipeId}')">Edit</button>
                                  <button class="btn btn-danger btn-sm"
                                      onclick="deleteRecipe('${recipe.recipeId}')">Delete</button>
                              </div>
                          </div>
                          <div class="row mt-2">
                              <div class="col-md-6">
                                  <h6>Chef's Note</h6>
                                  <p class="card-text">${recipe.summary || "No summary available."}</p>
                                  <h6>Ingredients</h6>
                                  <p class="card-text">${recipe.ingredients ? recipe.ingredients.join(", ") : "N/A"}</p>
                              </div>
                              <div class="col-md-6">
                                  <h6>Instructions</h6>
                                  <p class="card-text">${recipe.instructions ? recipe.instructions.join("<br>") : "N/A"}</p>
                              </div>
                          </div>
                      </div>
                      <div class="card-footer text-muted text-left" style="font-size: 0.85rem;">
                          <b>Categories: </b>${categoriesText}
                      </div>
                  </div>
              </div>`;
          cardContainer.innerHTML += card;
      });
  }
});