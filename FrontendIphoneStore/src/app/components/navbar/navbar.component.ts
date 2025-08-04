import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CartService } from '../../services/cart.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  currentUser: User | null = null;
  cartItemCount = 0;

  constructor(
    private authService: AuthService, 
    private router: Router,
    private cartService: CartService
  ) {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
      if (user) {
        this.cartService.initializeCart();
        this.cartService.cart$.subscribe(cart => {
          this.cartItemCount = cart.items.reduce((total, item) => total + item.quantity, 0);
        });
      }
    });
  }

  logout(): void {
    this.authService.logout();
    this.cartItemCount = 0;
    this.router.navigate(['/login']);
  }

  canViewUsers(): boolean {
    return this.currentUser?.role === 'Admin';
  }

  canManageProducts(): boolean {
    return this.currentUser?.role === 'Admin' || this.currentUser?.role === 'Seller';
  }
}
