
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
<<<<<<< HEAD
import { SidebarComponent } from './layout/sidebar/sidebar';
=======
>>>>>>> 8412c94f3493a4eae32fc655b5d9a87aa8bc7a9e

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
<<<<<<< HEAD
    SidebarComponent
=======
>>>>>>> 8412c94f3493a4eae32fc655b5d9a87aa8bc7a9e
  ],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
}
