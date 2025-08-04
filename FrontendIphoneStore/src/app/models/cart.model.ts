export interface CartItem {
  id: number;
  productVariationId: number;
  productName: string;
  color: string;
  price: number;
  quantity: number;
  subtotal: number;
}

export interface Cart {
  id: number;
  items: CartItem[];
  total: number;
}

export interface AddToCartRequest {
  productVariationId: number;
  quantity: number;
}