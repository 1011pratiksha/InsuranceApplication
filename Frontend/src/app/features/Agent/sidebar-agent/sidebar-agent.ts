import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  imports: [CommonModule,
    RouterLink,
    RouterLinkActive,
    RouterOutlet
],
  selector: 'app-sidebar-agent',
  styleUrl: './sidebar-agent.scss',
  templateUrl: './sidebar-agent.html',
})
export class SidebarAgent {
}
