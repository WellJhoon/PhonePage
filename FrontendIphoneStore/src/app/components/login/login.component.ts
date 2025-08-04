import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { LoginRequest } from '../../models/user.model';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginData: LoginRequest = {
    username: '',
    password: ''
  };
  
  errorMessage = '';
  loading = false;

  constructor(private authService: AuthService, private router: Router) {
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/products']);
    }
  }

  onSubmit(): void {
    if (!this.loginData.username || !this.loginData.password) {
      this.errorMessage = 'Por favor complete todos los campos';
      return;
    }

    this.loading = true;
    this.errorMessage = '';

    console.log('Intentando login con:', this.loginData);
    console.log('URL del backend:', 'http://localhost:5220/api/auth/login');

    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        console.log('Login exitoso:', response);
        this.router.navigate(['/products']);
      },
      error: (error) => {
        console.error('Error de login:', error);
        this.errorMessage = error.error?.message || `Error: ${error.status} - ${error.statusText}`;
        this.loading = false;
      }
    });
  }
}
