import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SupportService } from '../../services/support';

@Component({
  selector: 'app-customer-support',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './customer-support.html',
  styleUrl: './customer-support.scss',
})
export class CustomerSupport implements OnInit {
  tickets: any[] = [];
  customerId = 1;

  loading = false;
  saving = false;
  showForm = false;
  searched = false;

  searchText = '';
  errorMessage = '';
  successMessage = '';

  categories = [
    { id: 2, name: 'Technical Issue' },
    { id: 3, name: 'Billing' },
    { id: 4, name: 'Policy Query' },
    { id: 5, name: 'Claim Related' },
  ];

  ticket = this.createEmptyTicket();

  constructor(private supportService: SupportService) {}

  ngOnInit(): void {
    this.loadTickets();
  }

  private createEmptyTicket(): any {
    return {
      categoryId: 0,
      subject: '',
      description: '',
      priority: 'Medium',
    };
  }

  get filteredTickets(): any[] {
    const search = this.searchText.trim().toLowerCase();

    if (!search) {
      return this.tickets;
    }

    return this.tickets.filter((item) =>
      [
        item.ticketId,
        item.subject,
        item.description,
        item.status,
        item.priority,
        item.categoryName,
        item.categoryId,
      ].some((value) =>
        String(value ?? '')
          .toLowerCase()
          .includes(search),
      ),
    );
  }

  get openCount(): number {
    return this.tickets.filter((item) => item.status?.toLowerCase() === 'open').length;
  }

  get inProgressCount(): number {
    return this.tickets.filter((item) => item.status?.toLowerCase() === 'in progress').length;
  }

  get resolvedCount(): number {
    return this.tickets.filter((item) =>
      ['resolved', 'closed'].includes(item.status?.toLowerCase()),
    ).length;
  }

  loadTickets(): void {
    if (!Number.isInteger(Number(this.customerId)) || Number(this.customerId) <= 0) {
      this.errorMessage = 'Please enter a valid customer ID.';
      this.tickets = [];
      this.searched = true;
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.searched = true;

    this.supportService.getTickets().subscribe({
      next: (data: any[]) => {
        this.tickets = data.filter((item) => Number(item.customerId) === Number(this.customerId));

        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading tickets:', error);
        this.errorMessage = 'Unable to load tickets. Please try again.';
        this.loading = false;
      },
    });
  }

  openCreateForm(): void {
    this.ticket = this.createEmptyTicket();
    this.showForm = true;
    this.errorMessage = '';
    this.successMessage = '';
  }

  cancelForm(): void {
    this.showForm = false;
    this.ticket = this.createEmptyTicket();
    this.errorMessage = '';
  }

  createTicket(): void {
    const selectedCustomerId = Number(this.customerId);

    if (!Number.isInteger(selectedCustomerId) || selectedCustomerId <= 0) {
      this.errorMessage = 'Please enter a valid customer ID.';
      return;
    }

    if (
      !Number.isInteger(Number(this.ticket.categoryId)) ||
      Number(this.ticket.categoryId) <= 0 ||
      !this.ticket.subject.trim() ||
      !this.ticket.description.trim() ||
      !['Low', 'Medium', 'High'].includes(this.ticket.priority)
    ) {
      this.errorMessage = 'Please complete all required fields.';
      return;
    }

    const payload = {
      customerId: selectedCustomerId,
      categoryId: Number(this.ticket.categoryId),
      subject: this.ticket.subject.trim(),
      description: this.ticket.description.trim(),
      priority: this.ticket.priority,
      status: 'Open',
    };

    this.saving = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.supportService.createTicket(payload).subscribe({
      next: () => {
        this.saving = false;
        this.showForm = false;
        this.ticket = this.createEmptyTicket();
        this.successMessage = `Ticket created successfully for Customer ID ${selectedCustomerId}.`;

        this.loadTickets();
      },
      error: (error) => {
        console.error('Error creating ticket:', error);
        this.saving = false;
        this.errorMessage =
          typeof error.error === 'string'
            ? error.error
            : error.error?.message || 'Unable to create ticket. Please check your details.';
      },
    });
  }

  getStatusClass(status: string): string {
    return (status || 'Open').toLowerCase().replace(/\s+/g, '-');
  }

  getPriorityClass(priority: string): string {
    return (priority || 'Medium').toLowerCase();
  }
}
