import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

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
  styleUrls: ['./recipes-management.component.css']
})
export class RecipesManagementComponent implements OnInit {
  addUpdateRecipesForm!: FormGroup;
  currentActivity?: string;
  modalTitle = '';
  recipeMangementList: recipe[] = [
    {
      id: 1,
      title: 'Garlic Naan',
      ingredients: 'Garlic',
      instructions: 'Rosted',
      images: 'https://tse4.mm.bing.net/th/id/OIP.zdxosW-_XUolgKE46hzRLwHaHa?rs=1&pid=ImgDetMain'

    },
    {
      id: 2,
      title: 'Panner Tikka',
      ingredients: 'Panner',
      instructions: 'Rosted',
      images: 'https://tse4.mm.bing.net/th/id/OIP.DH0KMPXHqWYW28hEc7OXLQHaE8?rs=1&pid=ImgDetMain'

    },
    {
      id: 3,
      title: 'Mushroom Masala',
      ingredients: 'Mushroom',
      instructions: 'Fried',
      images: 'https://tse1.mm.bing.net/th/id/OIP._cFTEJfGskttbi2rmnRLUgHaE8?rs=1&pid=ImgDetMain'
    },
    {
      id: 4,
      title: 'Veg Noodles',
      ingredients: 'Noodles',
      instructions: 'Fried',
      images: 'https://th.bing.com/th/id/R.87c1ac385d7565caff0567f429c167b1?rik=Aq2lmNW1gmajFw&riu=http%3a%2f%2fwww.recipemasters.in%2fwp-content%2fuploads%2f2015%2f06%2fVeg-noodles.jpeg&ehk=1BIiWyzr4K8PxNnIJP2bSCAeMTkfvVDdvP5dk%2b3ygaQ%3d&risl=&pid=ImgRaw&r=0'
    }
  ];
  imageUrl: string | ArrayBuffer | null = null;
  file: File | null = null;

  constructor(private fb: FormBuilder) {

  }
  selectedFile: any | null = null;
  imagePath: string | null = null;
  ngOnInit(): void {
    this.createRecipesForm();

  }
  createRecipesForm() {
    this.addUpdateRecipesForm = this.fb.group({
      title: [''],
      ingredients: [''],
      instructions: [''],
      images: ['']

    })
  }
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.file = input.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        this.imageUrl = reader.result;
      };
      reader.readAsDataURL(this.file);
    }
  }
  setCurrentActivity(activity: string){
    if(activity === 'add'){
      this.modalTitle ='Add Recipe';
    } else{
      this.modalTitle ='Update Recipe';
    }
  }
  enableEdit(recipe: recipe) {
    if(recipe){
      this.addUpdateRecipesForm.patchValue({
        title:recipe.title,
        images:recipe.images,
        ingredients: recipe.ingredients,
        instructions: recipe.instructions
      });
    } else{
      this.addUpdateRecipesForm.reset();
    }
   
  }
  addupdateRecipes() {
    
    const recipesObj:recipe = {
      id: this.recipeMangementList.length+1,
      title: this.addUpdateRecipesForm.controls['title'].value,
      ingredients: this.addUpdateRecipesForm.controls['ingredients'].value,
      instructions: this.addUpdateRecipesForm.controls['instructions'].value,
      images: this.imageUrl
    }
   
    this.recipeMangementList.push(recipesObj);
    console.log(this.recipeMangementList)
  }
  deleteRecipe(id:number) {
    let newRecipeList = this.recipeMangementList.filter(recipe=> recipe.id !== id);
    this.recipeMangementList = newRecipeList;
    let newssd!:string;
  }
}
