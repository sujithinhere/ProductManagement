import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProductCreate } from './../../interfaces/product-create.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css']
})
export class ProductCreateComponent implements OnInit {
  public errorMessage: string = '';

  public productForm: FormGroup;
  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router) { }
  
  ngOnInit(){
    this.productForm = new FormGroup({
      productCode: new FormControl('', [Validators.required, Validators.maxLength(255)]),
      productName: new FormControl('', [Validators.required, Validators.maxLength(255)]),
      productUrl: new FormControl('', [Validators.required])
    });
  }

  public validateControl(controlName: string) {
    if (this.productForm.controls[controlName].invalid && this.productForm.controls[controlName].touched)
      return true;

    return false;
  }

  public hasError(controlName: string, errorName: string) {
    if (this.productForm.controls[controlName].hasError(errorName))
      return true;

    return false;
  }

  public createProduct(productFormValue) {
    if (this.productForm.valid) {
      this.executeProductCreation(productFormValue);
    }
  }
  
    private executeProductCreation(productFormValue) {
    let product: ProductCreate = {
      productCode: productFormValue.productCode,
      productName: productFormValue.productName,
      productUrl: productFormValue.productUrl
    }

      let apiUrl = 'api/product';
      this.repository.create(apiUrl, product)
        .subscribe(res => {
          $('#successModal').modal();
        },
          (error => {
            this.errorHandler.handleError(error);
            this.errorMessage = this.errorHandler.errorMessage;
          })
        )
    }

  public redirectToProductList() {
    this.router.navigate(['/product/list']);
  }

}
