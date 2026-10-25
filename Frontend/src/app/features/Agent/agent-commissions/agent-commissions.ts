
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AgentCommission } from '../../../services/AgentService/agent-commission';

@Component({
  selector: 'app-agent-commissions',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './agent-commissions.html',
  styleUrl: './agent-commissions.scss'
})
export class AgentCommissions implements OnInit {

  commissions: any[] = [];

  isLoading = false;
  showForm = false;
  policies: any[] = [];
  agents: any[] = [];

  // Filters
  filterStatus = '';
  filterAgentId: number | null = null;
  fromDate = '';
  toDate = '';

  // Calculate Commission Form
  agentId: number | null = null;
  policyId: number | null = null;

  constructor(private commissionService: AgentCommission) {}

  ngOnInit(): void {
    this.loadCommissions();
    this.loadPolicies();
    this.loadAgents();
  }

  // Load all commissions
  loadCommissions(): void {
    this.isLoading = true;

    this.commissionService.getAllCommissions(
      this.filterStatus || undefined,
      this.filterAgentId ?? undefined,
      this.fromDate || undefined,
      this.toDate || undefined
    ).subscribe({
      next: (response: any) => {
        this.commissions = Array.isArray(response)
          ? response
          : response?.data ?? [];

        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading commissions:', error);
        this.commissions = [];
        this.isLoading = false;
      }
    });
  }

  // Apply filters
  applyFilters(): void {
    this.loadCommissions();
  }

  // Reset filters
  resetFilters(): void {
    this.filterStatus = '';
    this.filterAgentId = null;
    this.fromDate = '';
    this.toDate = '';

    this.loadCommissions();
  }

  // Show or hide calculate commission form
  toggleForm(): void {
    this.showForm = !this.showForm;
  }

  // Calculate commission
  calculateCommission(): void {
    if (!this.agentId || !this.policyId) {
      alert('Please enter Agent ID and Policy ID.');
      return;
    }

    const data = {
      agentId: this.agentId,
      policyId: this.policyId
    };

    this.commissionService.calculateCommission(data).subscribe({
      next: () => {
        alert('Commission calculated successfully.');

        this.agentId = null;
        this.policyId = null;
        this.showForm = false;

        this.loadCommissions();
      },
      error: (error) => {
        console.error('Error calculating commission:', error);
        alert('Unable to calculate commission.');
      }
    });
  }

  // Update commission status
  updateStatus(commissionId: number, status: string): void {
    this.commissionService.updateCommissionStatus(
      commissionId,
      status
    ).subscribe({
      next: () => {
        alert('Commission status updated successfully.');
        this.loadCommissions();
      },
      error: (error) => {
        console.error('Error updating commission status:', error);
        alert('Unable to update commission status.');
      }
    });
  }


  loadPolicies(): void {
  this.commissionService.getAllPolicies().subscribe({
    next: (response: any) => {
      this.policies = Array.isArray(response)
        ? response
        : response?.data ?? [];
    },
    error: (error) => {
      console.error('Error loading policies:', error);
      this.policies = [];
    }
  });
}


loadAgents(): void {
  this.commissionService.getAllAgents().subscribe({
    next: (response: any) => {
      this.agents = Array.isArray(response)
        ? response
        : response?.data ?? [];
    },
    error: (error) => {
      console.error('Error loading agents:', error);
      this.agents = [];
    }
  });
}

}
