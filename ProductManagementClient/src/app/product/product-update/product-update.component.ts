import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { ErrorHandlerService } from './../../shared/services/error-handler.service';
import { RepositoryService } from './../../shared/services/repository.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/interfaces/product.model';

@Component({
  selector: 'app-product-update',
  templateUrl: './product-update.component.html',
  styleUrls: ['./product-update.component.css']
})
export class ProductUpdateComponent implements OnInit {

  public errorMessage: string = '';
  public product: Product;
  public productForm: FormGroup;

  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router, private activeRoute: ActivatedRoute) { }

  ngOnInit() {
    this.productForm = new FormGroup({
      productCode: new FormControl('', [Validators.required, Validators.maxLength(255)]),
      productName: new FormControl('', [Validators.required, Validators.maxLength(255)]),
      productUrl: new FormControl('', [Validators.required])
    });

    this.getProductById();
  }

  private getProductById() {
    let productCode: string = this.activeRoute.snapshot.params['productCode'];
    let productByIdUrl: string = `api/product/${productCode}`;

    this.repository.getData(productByIdUrl).subscribe(res => {
      this.product = res as Product;
      this.productForm.patchValue(this.product);
    },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      })
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

  public redirectToProductList() {
    this.router.navigate(['/product/list']);
  }

  public updateProduct(productFormValue) {
    if (this.productForm.valid) {
      this.executeOwnerUpdate(productFormValue);
    }
  }

  private executeOwnerUpdate(productFormValue) {

    this.product.productCode = productFormValue.productCode;
    this.product.productName = productFormValue.productName;
    this.product.productUrl = productFormValue.productUrl;

    let apiUrl = `api/product/${this.product.productCode}`;
    this.repository.update(apiUrl, this.product)
      .subscribe(res => {
        $('#successModal').modal();
      },
        (error => {
          this.errorHandler.handleError(error);
          this.errorMessage = this.errorHandler.errorMessage;
        })
      )
  }

}
