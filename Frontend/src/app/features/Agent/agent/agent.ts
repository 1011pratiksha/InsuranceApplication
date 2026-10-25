import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Agent as AgentService } from '../../../services/AgentService/agent';

@Component({
  selector: 'app-agent',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './agent.html',
  styleUrl: './agent.scss'
})
export class Agent implements OnInit {

 agents: any[] = [];
users: any[] = [];
  showForm = false;

  agent = {userId: 0, joiningDate: '', licenseNumber: '' };

  constructor(private agentService: AgentService) {}

  ngOnInit(): void {
    this.loadAgents();
    this.loadUsers();
  }

  loadAgents(): void {
    this.agentService.getAllAgents().subscribe({
      next: data => this.agents = data ?? [],
      error: error => console.error('Error loading agents:', error)
    });
  }

  
  loadUsers(): void {
    this.agentService.getAllUsers().subscribe({
      next: data => {
        this.users = data ?? [];
        console.log('Users loaded:', this.users);
      },
      error: error => console.error('Error loading users:', error)
    });
  }

  createAgent(): void {
    if (!this.agent.userId || !this.agent.joiningDate) {
      alert('Please enter User ID and Joining Date.');
      return;
    }

    const payload = {
      userId: Number(this.agent.userId),
      joiningDate: this.agent.joiningDate,
      licenseNumber: this.agent.licenseNumber || null
    };


    this.agentService.createAgent(payload).subscribe({
      next: () => {
        alert('Agent created successfully.');

        this.agent = {
          userId: 0,
          joiningDate: '',
          licenseNumber: ''
        };

        this.showForm = false;
        this.loadAgents();
      },
      error: error => {
        console.error('Error creating agent:', error);
        alert(error.error?.msg || 'Unable to create agent.');
      }
    });
  }
}