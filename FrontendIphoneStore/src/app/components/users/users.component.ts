import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService, UsersResponse } from '../../services/user.service';
import { AuthService } from '../../services/auth.service';
import { User, RegisterRequest, UserRole } from '../../models/user.model';

@Component({
  selector: 'app-users',
  imports: [CommonModule, FormsModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {
  users: User[] = [];
  currentUser: User | null = null;
  loading = false;
  showEditForm = false;
  editingUser: User | null = null;
  
  // Paginación
  currentPage = 1;
  pageSize = 10;
  totalPages = 0;
  totalCount = 0;
  
  // Búsqueda
  searchTerm = '';
  
  // Formulario
  userForm: RegisterRequest = {
    username: '',
    email: '',
    password: '',
    role: UserRole.User
  };
  
  UserRole = UserRole;

  constructor(
    private userService: UserService,
    private authService: AuthService
  ) {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.loading = true;
    this.userService.getUsers(this.currentPage, this.pageSize, this.searchTerm)
      .subscribe({
        next: (response: UsersResponse) => {
          this.users = response.users;
          this.totalPages = response.totalPages;
          this.totalCount = response.totalCount;
          this.loading = false;
        },
        error: () => {
          this.loading = false;
        }
      });
  }

  onSearch(): void {
    this.currentPage = 1;
    this.loadUsers();
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadUsers();
  }

  canManageUsers(): boolean {
    return this.currentUser?.role === 'Admin';
  }

  openEditForm(user: User): void {
    this.showEditForm = true;
    this.editingUser = user;
    this.userForm = {
      username: user.username,
      email: user.email,
      password: '',
      role: this.getRoleEnum(user.role)
    };
  }

  closeForm(): void {
    this.showEditForm = false;
    this.editingUser = null;
    this.resetForm();
  }

  resetForm(): void {
    this.userForm = {
      username: '',
      email: '',
      password: '',
      role: UserRole.User
    };
  }

  getRoleEnum(roleString: string): UserRole {
    switch (roleString) {
      case 'Admin': return UserRole.Admin;
      case 'Seller': return UserRole.Seller;
      default: return UserRole.User;
    }
  }

  getRoleString(role: UserRole): string {
    switch (role) {
      case UserRole.Admin: return 'Admin';
      case UserRole.Seller: return 'Seller';
      default: return 'User';
    }
  }

  onSubmit(): void {
    if (this.editingUser) {
      this.userService.updateUser(this.editingUser.id, this.userForm)
        .subscribe({
          next: () => {
            this.loadUsers();
            this.closeForm();
          }
        });
    }
  }

  deleteUser(user: User): void {
    if (confirm(`¿Está seguro de eliminar el usuario ${user.username}?`)) {
      this.userService.deleteUser(user.id)
        .subscribe({
          next: () => {
            this.loadUsers();
          }
        });
    }
  }
}
