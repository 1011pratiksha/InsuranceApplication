import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  {
    path: 'claims/details/:id',
    renderMode: RenderMode.Client
  },
  {
    path: 'admin/claims/details/:id',
    renderMode: RenderMode.Client
  },
  {
    path: 'customer/claims/details/:id',
    renderMode: RenderMode.Client
  },
  {
    path: 'agent/claims/details/:id',
    renderMode: RenderMode.Client
  },
  {
    path: '**',
    renderMode: RenderMode.Prerender
  }
];
