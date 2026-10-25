
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Agent } from '../../../services/AgentService/agent';
import { AgentPolicy } from '../../../services/AgentService/agent-policy';

@Component({
  selector: 'app-agent-policies',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './agent-policies.html',
  styleUrl: './agent-policies.scss'
})
export class AgentPolicies implements OnInit {

  agents: any;
  customers: any;
  policies: any;
  agentPolicies: any;

  selectedAgentId = 0;
  showForm = false;

  agentPolicy = {
    agentId: 0,
    customerId: 0,
    policyId: 0,
    saleDate: ''
  };

  constructor(
    private agentService: Agent,
    private policyService: AgentPolicy
  ) {}

  ngOnInit(): void {
    this.loadAgents();
    this.loadCustomers();
    this.loadPolicies();
  }

  // Load agents
  loadAgents(): void {
    this.agentService.getAllAgents().subscribe({
      next: (data) => {
        this.agents = data;
      },
      error: (error) => {
        console.error('Error loading agents:', error);
        alert('Unable to load agents.');
      }
    });
  }

  // Load customers for dropdown
  loadCustomers(): void {
    this.policyService.getAllCustomers().subscribe({
      next: (data) => {
        this.customers = data;
      },
      error: (error) => {
        console.error('Error loading customers:', error);
        alert('Unable to load customers.');
      }
    });
  }

  // Load policies for dropdown
  loadPolicies(): void {
    this.policyService.getAllPolicies().subscribe({
      next: (data) => {
        this.policies = data;
      },
      error: (error) => {
        console.error('Error loading policies:', error);
        alert('Unable to load policies.');
      }
    });
  }

  // Load policies assigned to selected agent
  onAgentChange(): void {
    if (Number(this.selectedAgentId) === 0) {
      this.agentPolicies = [];
      return;
    }

    this.policyService
      .getAgentPolicies(Number(this.selectedAgentId))
      .subscribe({
        next: (data) => {
          this.agentPolicies = data;
        },
        error: (error) => {
          console.error('Error loading agent policies:', error);
          this.agentPolicies = [];
        }
      });
  }

  // Show or hide assignment form
  toggleForm(): void {
    this.showForm = !this.showForm;
  }

  // Assign policy to agent
  assignPolicy(): void {
    const agentId = Number(this.agentPolicy.agentId);
    const customerId = Number(this.agentPolicy.customerId);
    const policyId = Number(this.agentPolicy.policyId);

    if (agentId <= 0 || customerId <= 0 || policyId <= 0) {
      alert('Please select an Agent, Customer and Policy.');
      return;
    }

    const data = {
      customerId: customerId,
      policyId: policyId,
      saleDate: this.agentPolicy.saleDate || null
    };

    this.policyService.createAgentPolicy(agentId, data).subscribe({
      next: () => {
        alert('Policy assigned successfully.');

        this.selectedAgentId = agentId;

        this.agentPolicy = {
          agentId: 0,
          customerId: 0,
          policyId: 0,
          saleDate: ''
        };

        this.showForm = false;
        this.onAgentChange();
      },
      error: (error) => {
        console.error('Error assigning policy:', error);
        alert('Unable to assign policy. Please check the API response.');
      }
    });
  }
}
