import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-feature-placeholder',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './feature-placeholder.component.html',
  styleUrl: './feature-placeholder.component.scss'
})
export class FeaturePlaceholderComponent {
  private route = inject(ActivatedRoute);

  get title(): string {
    return this.route.snapshot.data['title'] || 'Coming Soon';
  }

  get category(): string {
    return this.route.snapshot.data['category'] || '';
  }

  get icon(): string {
    return this.route.snapshot.data['icon'] || 'construction';
  }

  get description(): string {
    return this.route.snapshot.data['description'] || 'This feature is currently under development and will be available soon.';
  }
}
