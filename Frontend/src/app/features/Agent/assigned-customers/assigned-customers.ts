
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AssignedCustomers } from '../../../services/AgentService/assigned-customers';

@Component({
  selector: 'app-assigned-customers',
  imports: [CommonModule, FormsModule],
  templateUrl: './assigned-customers.html',
  styleUrl: './assigned-customers.scss'
})
export class AssignedCustomersComponent {

  customers: any;
  availableCustomers: any;
  agents: any;

  customerId = 0;
  agentId = 1;

  transferCustomerId = 0;
  newAgentId = 0;

  constructor(private service: AssignedCustomers) {
    this.loadCustomers();
    this.loadAvailableCustomers();
    this.loadAgents();
  }

  // Current agent che assigned customers
  loadCustomers() {
    this.service.getCustomers(this.agentId).subscribe({
      next: (data: any) => {
        this.customers = data;
      },
      error: (error) => {
        console.error('Error loading assigned customers:', error);
      }
    });
  }

  // All customers for dropdown
  loadAvailableCustomers() {
    this.service.getAllCustomers().subscribe({
      next: (data: any) => {
        this.availableCustomers = data;
      },
      error: (error) => {
        console.error('Error loading available customers:', error);
      }
    });
  }

  // All agents for transfer dropdown
  loadAgents() {
    this.service.getAllAgents().subscribe({
      next: (data: any) => {
        this.agents = data;
      },
      error: (error) => {
        console.error('Error loading agents:', error);
      }
    });
  }

  // Assign customer
  assignCustomer() {
    if (!this.customerId) {
      alert('Please select a customer.');
      return;
    }

    const data = {
      customerId: Number(this.customerId)
    };

    this.service.assignCustomer(this.agentId, data).subscribe({
      next: () => {
        alert('Customer assigned successfully');
        this.customerId = 0;
        this.loadCustomers();
        this.loadAvailableCustomers();
      },
      error: (error) => {
        alert(error.error?.msg || 'Customer assignment failed');
      }
    });
  }

  // Transfer customer
  transferCustomer() {
    if (!this.transferCustomerId || !this.newAgentId) {
      alert('Please select a customer and a new agent.');
      return;
    }

    const data = {
      newAgentId: Number(this.newAgentId)
    };

    this.service
      .transferCustomer(this.agentId, this.transferCustomerId, data)
      .subscribe({
        next: () => {
          alert('Customer transferred successfully');
          this.transferCustomerId = 0;
          this.newAgentId = 0;
          this.loadCustomers();
          this.loadAvailableCustomers();
        },
        error: (error) => {
          alert(error.error?.msg || 'Customer transfer failed');
        }
      });
  }
}
