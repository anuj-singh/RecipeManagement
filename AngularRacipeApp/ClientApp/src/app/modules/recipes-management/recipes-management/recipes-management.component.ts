import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-recipes-management',
  templateUrl: './recipes-management.component.html',
  styleUrls: ['./recipes-management.component.css']
})
export class RecipesManagementComponent implements OnInit {
  addUpdateRecipesForm!: FormGroup;
  recipeMangementList = [
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
  ]

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
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0].name;
      console.log(this.selectedFile.name)
    }
  }
  addupdateRecipes() {
    const recipesObj = {
      title: this.addUpdateRecipesForm.controls['title'],
      ingredients: this.addUpdateRecipesForm.controls['ingredients'],
      instructions: this.addUpdateRecipesForm.controls['instructions'],
      images: this.selectedFile
    }
  }

}
