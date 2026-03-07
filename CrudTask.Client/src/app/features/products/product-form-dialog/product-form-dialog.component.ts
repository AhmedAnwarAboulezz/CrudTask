import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { Product } from '../../../core/models/product.model';
import { ProductService } from '../../../core/services/product.service';

@Component({
  selector: 'app-product-form-dialog',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
  ],
  templateUrl: './product-form-dialog.component.html',
  styleUrl: './product-form-dialog.component.scss',
})
export class ProductFormDialogComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly productService = inject(ProductService);
  private readonly dialogRef = inject(MatDialogRef<ProductFormDialogComponent>);
  readonly existingProduct: Product | null = inject(MAT_DIALOG_DATA);

  form!: FormGroup;
  isSubmitting = false;

  readonly currencies = ['USD', 'EUR', 'GBP', 'EGP'];

  get isEditMode(): boolean {
    return this.existingProduct !== null;
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      name: [this.existingProduct?.name ?? '', [Validators.required, Validators.maxLength(200)]],
      description: [this.existingProduct?.description ?? '', Validators.required],
      price: [this.existingProduct?.price ?? 0, [Validators.required, Validators.min(0)]],
      currency: [this.existingProduct?.currency ?? 'USD', Validators.required],
      category: [this.existingProduct?.category ?? '', Validators.required],
    });
  }

  submit(): void {
    if (this.form.invalid) return;

    this.isSubmitting = true;
    const formValue = this.form.getRawValue();

    const onSuccess = () => {
      this.isSubmitting = false;
      this.dialogRef.close(true);
    };

    const onError = () => {
      this.isSubmitting = false;
    };

    if (this.isEditMode) {
      this.productService
        .update(this.existingProduct!.id, { id: this.existingProduct!.id, ...formValue })
        .subscribe({ next: onSuccess, error: onError });
    } else {
      this.productService
        .create(formValue)
        .subscribe({ next: onSuccess, error: onError });
    }
  }

  cancel(): void {
    this.dialogRef.close(false);
  }
}
