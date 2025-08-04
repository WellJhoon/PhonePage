import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Cart, AddToCartRequest } from '../models/cart.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'http://localhost:5220/api/cart';
  private cartSubject = new BehaviorSubject<Cart>({ id: 0, items: [], total: 0 });
  public cart$ = this.cartSubject.asObservable();

  constructor(private http: HttpClient) {}

  initializeCart(): void {
    this.loadCart();
  }

  loadCart(): void {
    this.http.get<Cart>(this.apiUrl).subscribe({
      next: (cart) => {
        this.cartSubject.next(cart);
      },
      error: (error) => {
        console.error('Error al cargar carrito:', error);
      }
    });
  }

  addToCart(request: AddToCartRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, request).pipe(
      tap(() => this.loadCart())
    );
  }

  removeFromCart(itemId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/item/${itemId}`).pipe(
      tap(() => this.loadCart())
    );
  }

  checkout(): Observable<any> {
    return this.http.post(`${this.apiUrl}/checkout`, {}).pipe(
      tap(() => this.loadCart())
    );
  }

  getCartItemCount(): number {
    return this.cartSubject.value.items.reduce((total, item) => total + item.quantity, 0);
  }
}