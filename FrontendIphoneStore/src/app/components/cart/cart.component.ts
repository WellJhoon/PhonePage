import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CartService } from '../../services/cart.service';
import { Cart } from '../../models/cart.model';

@Component({
  selector: 'app-cart',
  imports: [CommonModule, RouterModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit {
  cart: Cart = { id: 0, items: [], total: 0 };

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cartService.cart$.subscribe(cart => {
      this.cart = cart;
    });
  }

  removeFromCart(itemId: number): void {
    this.cartService.removeFromCart(itemId).subscribe();
  }

  checkout(): void {
    if (this.cart.items.length === 0) {
      return;
    }
    
    this.cartService.checkout().subscribe({
      next: (response) => {
        alert('FELICIDADES POR SU COMPRA');
      },
      error: (error) => {
        console.error('Error al procesar la compra');
        alert(error.error?.message || 'Error al procesar la compra');
      }
    });
  }
}
