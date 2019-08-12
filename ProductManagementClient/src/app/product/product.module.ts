import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from './../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';

import { ProductListComponent } from './product-list/product-list.component';
import { ProductCreateComponent } from './product-create/product-create.component';
import { ProductUpdateComponent } from './product-update/product-update.component';
import { ProductDeleteComponent } from './product-delete/product-delete.component';
import { ProductDuplicateComponent } from './product-duplicate/product-duplicate.component';

@NgModule({
    imports: [
        CommonModule,
        SharedModule,
        ReactiveFormsModule,
        RouterModule.forChild([
            { path: 'list', component: ProductListComponent },
            { path: 'create', component: ProductCreateComponent },
            { path:'update/:productCode', component: ProductUpdateComponent },
            { path:'delete/:productCode', component: ProductDeleteComponent }
        ])
    ],
    declarations: [ProductListComponent, ProductCreateComponent, ProductUpdateComponent, ProductDeleteComponent, ProductDuplicateComponent]
})
export class ProductModule { }