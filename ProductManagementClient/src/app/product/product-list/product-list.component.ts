import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { RepositoryService } from './../../shared/services/repository.service';
import { Product } from './../../interfaces/product.model';
import { ErrorHandlerService } from './../../shared/services/error-handler.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
public products: Product[];
  public errorMessage: string = '';

  constructor(private repository: RepositoryService, private errorHandler: ErrorHandlerService, 
    private router: Router) { }

  ngOnInit() {
    this.getAllProducts();
  }

  public getAllProducts(){
    let apiUrl: string = "api/product";
    this.repository.getData(apiUrl)
    .subscribe(result => {
      this.products = result as Product[];
    },
    (error) => {
      this.errorHandler.handleError(error);
      this.errorMessage = this.errorHandler.errorMessage;
    }
    )
  }

  public redirectToUpdatePage(productCode){
    let updateUrl: string = `/product/update/${productCode}`;
    this.router.navigate([updateUrl]);
  }

  public redirectToDeletePage(productCode)
  {
    let deleteUrl: string = `/product/delete/${productCode}`;
    this.router.navigate([deleteUrl]);
  }
}
