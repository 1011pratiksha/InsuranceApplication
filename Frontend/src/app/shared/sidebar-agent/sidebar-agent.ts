import { Component } from '@angular/core';
import {
  RouterLink,
  RouterLinkActive,
  RouterOutlet
} from '@angular/router';

@Component({
  selector: 'app-sidebar-agent',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    RouterOutlet
  ],
  styleUrl: './sidebar-agent.scss',
  templateUrl: './sidebar-agent.html',
})
export class SidebarAgent {
}