import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SupportService } from '../../services/support';

@Component({
  selector: 'app-support',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './support.html',
  styleUrl: './support.scss',
})
export class Support implements OnInit {
  tickets: any[] = [];
  loading = false;
  errorMessage = '';
  successMessage = '';
  searchText = '';
  editingTicketId: number | null = null;
  showForm = false;

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
      customerId: 0,
      categoryId: 0,
      subject: '',
      description: '',
      status: 'Open',
      priority: 'Medium',
    };
  }

  get filteredTickets(): any[] {
    const search = this.searchText.trim().toLowerCase();

    if (!search) {
      return this.tickets;
    }

    return this.tickets.filter((item) =>
      [item.ticketId, item.customerId, item.subject, item.status, item.priority].some((value) =>
        String(value ?? '')
          .toLowerCase()
          .includes(search),
      ),
    );
  }

  get totalTickets(): number {
    return this.tickets.length;
  }

  get openTickets(): number {
    return this.tickets.filter((item) => item.status?.toLowerCase() === 'open').length;
  }

  get inProgressTickets(): number {
    return this.tickets.filter((item) => item.status?.toLowerCase() === 'in progress').length;
  }

  get resolvedTickets(): number {
    return this.tickets.filter((item) =>
      ['resolved', 'closed'].includes(item.status?.toLowerCase()),
    ).length;
  }

  loadTickets(): void {
    this.loading = true;
    this.errorMessage = '';

    this.supportService.getTickets().subscribe({
      next: (data) => {
        this.tickets = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading tickets:', error);
        this.errorMessage = 'Tickets could not be loaded.';
        this.loading = false;
      },
    });
  }

  openCreateForm(): void {
    this.ticket = this.createEmptyTicket();
    this.editingTicketId = null;
    this.showForm = true;
    this.errorMessage = '';
    this.successMessage = '';
  }

  editTicket(item: any): void {
    this.ticket = {
      customerId: item.customerId,
      categoryId: item.categoryId,
      subject: item.subject ?? '',
      description: item.description ?? '',
      status: item.status ?? 'Open',
      priority: item.priority ?? 'Medium',
    };

    this.editingTicketId = item.ticketId;
    this.showForm = true;
    this.errorMessage = '';
    this.successMessage = '';
  }

  cancelForm(): void {
    this.showForm = false;
    this.editingTicketId = null;
    this.ticket = this.createEmptyTicket();
    this.errorMessage = '';
  }

  saveTicket(): void {
    if (
      !Number.isInteger(Number(this.ticket.customerId)) ||
      Number(this.ticket.customerId) <= 0 ||
      !Number.isInteger(Number(this.ticket.categoryId)) ||
      Number(this.ticket.categoryId) <= 0 ||
      !this.ticket.subject.trim() ||
      !this.ticket.description.trim()
    ) {
      this.errorMessage = 'Enter valid customer and category IDs and complete all required fields.';
      return;
    }

    const payload = {
      customerId: Number(this.ticket.customerId),
      categoryId: Number(this.ticket.categoryId),
      subject: this.ticket.subject.trim(),
      description: this.ticket.description.trim(),
      status: this.ticket.status,
      priority: this.ticket.priority,
    };

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const request =
      this.editingTicketId === null
        ? this.supportService.createTicket(payload)
        : this.supportService.updateTicket(this.editingTicketId, payload);

    request.subscribe({
      next: () => {
        this.successMessage =
          this.editingTicketId === null
            ? 'Ticket created successfully.'
            : 'Ticket updated successfully.';

        this.showForm = false;
        this.editingTicketId = null;
        this.ticket = this.createEmptyTicket();
        this.loadTickets();
      },
      error: (error) => {
        console.error('Error saving ticket:', error);
        this.errorMessage = error.error?.message || error.error || 'Ticket could not be saved.';
        this.loading = false;
      },
    });
  }

  deleteTicket(id: number): void {
    const confirmed = confirm(`Are you sure you want to delete ticket ${id}?`);

    if (!confirmed) {
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.supportService.deleteTicket(id).subscribe({
      next: () => {
        this.tickets = this.tickets.filter((item) => item.ticketId !== id);
        this.loading = false;
        this.successMessage = 'Ticket deleted successfully.';
      },
      error: (error) => {
        console.error('Error deleting ticket:', error);
        this.loading = false;
        this.errorMessage =
          typeof error.error === 'string'
            ? error.error
            : error.error?.message || 'Ticket could not be deleted.';
      },
    });
  }
}
