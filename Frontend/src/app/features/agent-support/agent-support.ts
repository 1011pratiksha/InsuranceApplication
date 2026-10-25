import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { SupportService } from '../../services/support';

@Component({
  selector: 'app-agent-support',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './agent-support.html',
  styleUrl: './agent-support.scss',
})
export class AgentSupport implements OnInit {
  tickets: any[] = [];
  filteredTickets: any[] = [];

  loading = false;
  savingId: number | null = null;

  searchText = '';
  statusFilter = '';
  priorityFilter = '';

  errorMessage = '';
  successMessage = '';

  statuses = ['Open', 'In Progress', 'Resolved', 'Closed'];
  priorities = ['Low', 'Medium', 'High'];

  constructor(private supportService: SupportService) {}

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets(): void {
    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.supportService.getTickets().subscribe({
      next: (data: any[]) => {
        this.tickets = data.map((ticket) => ({ ...ticket }));
        this.applyFilters();
        this.loading = false;
      },
      error: (error: HttpErrorResponse) => {
        console.error('Failed to load support tickets:', error);

        this.errorMessage =
          `Unable to load support tickets. HTTP ${error.status || 'Unknown'}: ` +
          (error.message || 'Request failed.');

        this.loading = false;
      },
    });
  }

  applyFilters(): void {
    const search = this.searchText.trim().toLowerCase();

    this.filteredTickets = this.tickets.filter((ticket) => {
      const id = ticket.ticketId ?? ticket.id ?? ticket.supportId ?? '';

      const matchesSearch =
        !search ||
        String(id).toLowerCase().includes(search) ||
        String(ticket.subject ?? '')
          .toLowerCase()
          .includes(search) ||
        String(ticket.description ?? '')
          .toLowerCase()
          .includes(search) ||
        String(ticket.customerId ?? '')
          .toLowerCase()
          .includes(search);

      const matchesStatus = !this.statusFilter || ticket.status === this.statusFilter;

      const matchesPriority = !this.priorityFilter || ticket.priority === this.priorityFilter;

      return matchesSearch && matchesStatus && matchesPriority;
    });
  }

  saveChanges(ticket: any): void {
    const id = Number(ticket.ticketId ?? ticket.id ?? ticket.supportId);

    if (!id) {
      this.errorMessage = 'Ticket ID is missing.';
      return;
    }

    const payload = {
      customerId: ticket.customerId,
      categoryId: ticket.categoryId,
      subject: ticket.subject,
      description: ticket.description,
      status: ticket.status,
      priority: ticket.priority,
    };

    this.savingId = id;
    this.errorMessage = '';
    this.successMessage = '';

    this.supportService.updateTicket(id, payload).subscribe({
      next: () => {
        this.savingId = null;
        this.successMessage = `Ticket #${id} updated successfully.`;
        this.loadTickets();
      },
      error: (error: HttpErrorResponse) => {
        console.error('Failed to update ticket:', error);

        this.savingId = null;
        this.errorMessage =
          `Unable to update ticket #${id}. HTTP ${error.status || 'Unknown'}: ` +
          (error.message || 'Request failed.');
      },
    });
  }

  get openCount(): number {
    return this.tickets.filter((ticket) => ticket.status === 'Open').length;
  }

  get inProgressCount(): number {
    return this.tickets.filter((ticket) => ticket.status === 'In Progress').length;
  }

  get resolvedCount(): number {
    return this.tickets.filter(
      (ticket) => ticket.status === 'Resolved' || ticket.status === 'Closed',
    ).length;
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Open':
        return 'text-bg-warning';
      case 'In Progress':
        return 'text-bg-primary';
      case 'Resolved':
        return 'text-bg-success';
      case 'Closed':
        return 'text-bg-secondary';
      default:
        return 'text-bg-light';
    }
  }

  getPriorityClass(priority: string): string {
    switch (priority) {
      case 'High':
        return 'text-bg-danger';
      case 'Medium':
        return 'text-bg-warning';
      case 'Low':
        return 'text-bg-info';
      default:
        return 'text-bg-light';
    }
  }
}
