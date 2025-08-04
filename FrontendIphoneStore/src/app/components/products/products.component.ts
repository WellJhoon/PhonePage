import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { AuthService } from '../../services/auth.service';
import { CartService } from '../../services/cart.service';
import { Product, ProductRequest, ProductVariationRequest } from '../../models/product.model';
import { User } from '../../models/user.model';
import { AddToCartRequest } from '../../models/cart.model';

@Component({
  selector: 'app-products',
  imports: [CommonModule, FormsModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  currentUser: User | null = null;
  loading = false;
  showCreateForm = false;
  editingProduct: Product | null = null;
  
  // Paginación
  currentPage = 1;
  pageSize = 6;
  totalPages = 0;
  totalCount = 0;
  
  // Búsqueda
  searchTerm = '';
  
  // Formulario
  productForm: ProductRequest = {
    name: '',
    description: '',
    imageUrl: '',
    variations: []
  };
  
  newVariation: ProductVariationRequest = {
    color: '',
    price: 0,
    stock: 0
  };
  
  selectedVariations: { [productId: number]: number } = {};

  constructor(
    private productService: ProductService,
    private authService: AuthService,
    private cartService: CartService
  ) {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });
  }

  ngOnInit(): void {
    this.loadProducts();
    // Inicializar selectedVariations
    this.products.forEach(product => {
      this.selectedVariations[product.id] = 0;
    });
  }

  loadProducts(): void {
    this.loading = true;
    this.productService.getProducts(this.currentPage, this.pageSize, this.searchTerm)
      .subscribe({
        next: (response) => {
          this.products = response.products;
          this.totalPages = response.totalPages;
          this.totalCount = response.totalCount;
          this.loading = false;
          // Inicializar selectedVariations para cada producto
          this.products.forEach(product => {
            if (!this.selectedVariations[product.id]) {
              this.selectedVariations[product.id] = 0;
            }
          });
        },
        error: () => {
          this.loading = false;
        }
      });
  }

  onSearch(): void {
    this.currentPage = 1;
    this.loadProducts();
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadProducts();
  }

  canManageProducts(): boolean {
    return this.currentUser?.role === 'Admin' || this.currentUser?.role === 'Seller';
  }

  canDeleteProducts(): boolean {
    return this.currentUser?.role === 'Admin';
  }

  openCreateForm(): void {
    this.showCreateForm = true;
    this.editingProduct = null;
    this.resetForm();
  }

  openEditForm(product: Product): void {
    this.showCreateForm = true;
    this.editingProduct = product;
    this.productForm = {
      name: product.name,
      description: product.description,
      imageUrl: product.imageUrl,
      variations: [...product.variations]
    };
  }

  closeForm(): void {
    this.showCreateForm = false;
    this.editingProduct = null;
    this.resetForm();
  }

  resetForm(): void {
    this.productForm = {
      name: '',
      description: '',
      imageUrl: '',
      variations: []
    };
    this.newVariation = {
      color: '',
      price: 0,
      stock: 0
    };
  }

  addVariation(): void {
    if (this.newVariation.color && this.newVariation.price > 0) {
      this.productForm.variations.push({ ...this.newVariation });
      this.newVariation = { color: '', price: 0, stock: 0 };
    }
  }

  removeVariation(index: number): void {
    this.productForm.variations.splice(index, 1);
  }

  onSubmit(): void {
    if (this.editingProduct) {
      this.productService.updateProduct(this.editingProduct.id, this.productForm)
        .subscribe({
          next: () => {
            this.loadProducts();
            this.closeForm();
          }
        });
    } else {
      this.productService.createProduct(this.productForm)
        .subscribe({
          next: () => {
            this.loadProducts();
            this.closeForm();
          }
        });
    }
  }

  deleteProduct(product: Product): void {
    if (confirm(`¿Está seguro de eliminar ${product.name}?`)) {
      this.productService.deleteProduct(product.id)
        .subscribe({
          next: () => {
            this.loadProducts();
          }
        });
    }
  }

  addToCart(productId: number): void {
    const variationId = this.selectedVariations[productId];
    if (!variationId) {
      alert('Por favor selecciona un color');
      return;
    }

    const request: AddToCartRequest = {
      productVariationId: variationId,
      quantity: 1
    };

    this.cartService.addToCart(request).subscribe({
      next: () => {
        this.selectedVariations[productId] = 0;
        this.loadProducts(); // Recargar para actualizar stock mostrado
      },
      error: (error) => {
        console.error('Error al agregar al carrito:', error);
        alert(error.error?.message || 'Error al agregar al carrito');
      }
    });
  }
}
