export interface Product {
  id: number;
  name: string;
  description: string;
  imageUrl: string;
  createdAt: string;
  variations: ProductVariation[];
}

export interface ProductVariation {
  id: number;
  color: string;
  price: number;
  stock: number;
}

export interface ProductRequest {
  name: string;
  description: string;
  imageUrl: string;
  variations: ProductVariationRequest[];
}

export interface ProductVariationRequest {
  color: string;
  price: number;
  stock: number;
}

export interface ProductsResponse {
  products: Product[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
}