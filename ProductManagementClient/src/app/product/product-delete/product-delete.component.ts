import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/interfaces/product.model';
import { RepositoryService } from 'src/app/shared/services/repository.service';
import { ErrorHandlerService } from 'src/app/shared/services/error-handler.service';

@Component({
  selector: 'app-product-delete',
  templateUrl: './product-delete.component.html',
  styleUrls: ['./product-delete.component.css']
})
export class ProductDeleteComponent implements OnInit {
  public errorMessage: string = '';
  public product: Product;

  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, private router: Router, private activeRoute: ActivatedRoute) { }

  ngOnInit() {
    this.getProductById();
  }

  private getProductById() {
    let productCode: string = this.activeRoute.snapshot.params['productCode'];
    let productByIdUrl: string = `api/product/${productCode}`;

    this.repository.getData(productByIdUrl).subscribe(res => {
      this.product = res as Product;      
    },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      })
  }

  private deleteProduct() {
    let productByIdUrl: string = `api/product/${this.product.productCode}`;

    this.repository.delete(productByIdUrl).subscribe(res => {
      $('#successModal').modal();
    },
      (error) => {
        this.errorHandler.handleError(error);
        this.errorMessage = this.errorHandler.errorMessage;
      })
  }

  public redirectToProductList() {
    this.router.navigate(['/product/list']);
  }
}
