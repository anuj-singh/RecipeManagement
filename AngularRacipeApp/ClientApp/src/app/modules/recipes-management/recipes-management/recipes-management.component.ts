import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DataService } from 'src/app/shared/service/data.service';

export interface recipe {
  id: number;
  title?: string;
  ingredients?: string;
  instructions?: string;
  images: string | ArrayBuffer | null;
}
@Component({
  selector: 'app-recipes-management',
  templateUrl: './recipes-management.component.html',
  styleUrls: ['./recipes-management.component.css'],
})
export class RecipesManagementComponent implements OnInit {
  userDetails: any = sessionStorage.getItem('tokenKey');
  loggedInUser: any;

  addUpdateRecipesForm!: FormGroup;
  currentActivity?: string;
  modalTitle = '';
  recipeMangementList: any[] = [];

  imageUrl: string | ArrayBuffer | null = null;
  fileName: any = '';

  //search and update recipe
  searchRecipesByIngredients: any = '';
  recipes:any;
  //end

  constructor(private fb: FormBuilder, private dataService: DataService) {}
  selectedFile: any | null = null;
  imagePath: string | null = null;
  ngOnInit(): void {
    if (this.userDetails) {
      this.loggedInUser = JSON.parse(this.userDetails);
    }
    this.createRecipesForm();
    this.getAllRecipes();
  }

  createRecipesForm() {
    this.addUpdateRecipesForm = this.fb.group({
      title: [''],
      ingredients: [''],
      instructions: [''],
      imageUrl: [''],
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.fileName = input.files[0]['name'];
      console.log(this.fileName);

      const reader = new FileReader();
      reader.onload = () => {
        this.imageUrl = reader.result;
      };
      // reader.readAsDataURL(this.fileName);
    }
  }
  setCurrentActivity(activity: string) {
    if (activity === 'add') {
      this.modalTitle = 'Add Recipe';
      this.addUpdateRecipesForm.reset();
    } else {
      this.modalTitle = 'Update Recipe';
    }
  }

  enableEdit(recipe: recipe) {
    this.recipes=recipe;
    if (recipe.id) {
      this.addUpdateRecipesForm.patchValue({
        title: recipe.title,
        images: recipe.images,
        ingredients: recipe.ingredients,
        instructions: recipe.instructions,
      });
    } else {
      this.addUpdateRecipesForm.reset();
    }
  }

  addupdateRecipes() {
    if(this.recipes.recipeId){
      // this.dataService
      // .httpUpdateRequest('Recipes/' + this.recipes.recipeId,this.recipes)
      // .subscribe((res: any) => {
      //   alert(res.message);
      //   this.getAllRecipes();
      // });
    }
    const recipesObj = {
      title: this.addUpdateRecipesForm.controls['title'].value,
      ingredients: this.addUpdateRecipesForm.controls['ingredients'].value,
      instructions: this.addUpdateRecipesForm.controls['instructions'].value,
      imageUrl: this.fileName,
      userId: this.loggedInUser.userId,
      description: 'Good forhealth',
      cookingTime: 45,
      statusId: 1,
      categoryId: 1,
    };

    this.dataService
      .httpPostRequest('Recipes', recipesObj)
      .subscribe((res: any) => {
        alert(res.message);
        this.getAllRecipes();
      });
  }

  deleteRecipe(recipe: any) {
    if (confirm('Are you sure wants to delete this recipe?')) {
      this.dataService
      .httpDeleteRequest('Recipes/' + recipe.recipeId)
      .subscribe((res: any) => {
        alert(res.message);
        this.getAllRecipes();
      });
    }
  }

  getAllRecipes() {
    this.dataService.httpGetRequest('Recipes').subscribe((res: any) => {
      this.recipeMangementList = res;
    });
  }

  // Search Recipe  by ingredients
  onSearchRecipes() {
    const searchRecipeObj = {
      title: '',
      ingredients: this.searchRecipesByIngredients,
      userId: 0,
      categoryId: 0
    };

    if (searchRecipeObj.ingredients.length > 2) {
      this.dataService
        .httpPostRequest('Recipes/GetRecipeDetailsForSearch', searchRecipeObj)
        .subscribe((res: any) => {
          this.recipeMangementList = [];
          this.recipeMangementList = res;
        });
    }
  }
  refreshPage() {
    this.searchRecipesByIngredients = '';
    this.getAllRecipes();
  }
  //end

  
  
}
