import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ProductsComponent } from './components/products/products.component';
import { UsersComponent } from './components/users/users.component';
import { CartComponent } from './components/cart/cart.component';
import { AuthGuard } from './guards/auth.guard';
import { RoleGuard } from './guards/role.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { 
    path: 'products', 
    component: ProductsComponent,
    canActivate: [AuthGuard]
  },
  { 
    path: 'users', 
    component: UsersComponent, 
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Admin'] }
  },
  { 
    path: 'cart', 
    component: CartComponent, 
    canActivate: [AuthGuard]
  },
  { path: '**', redirectTo: '/products' }
];
