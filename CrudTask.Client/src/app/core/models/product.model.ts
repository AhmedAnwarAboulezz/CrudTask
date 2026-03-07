export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  currency: string;
  category: string;
  createdAt: string;
  updatedAt: string;
}

export interface CreateProductRequest {
  name: string;
  description: string;
  price: number;
  currency: string;
  category: string;
}

export interface UpdateProductRequest {
  id: string;
  name: string;
  description: string;
  price: number;
  currency: string;
  category: string;
}
