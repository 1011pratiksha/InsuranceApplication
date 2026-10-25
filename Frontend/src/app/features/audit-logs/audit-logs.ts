import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuditLog } from '../../services/audit-log';

@Component({
  selector: 'app-audit-logs',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './audit-logs.html',
  styleUrl: './audit-logs.scss',
})
export class AuditLogs implements OnInit {
  logs: any[] = [];
  filteredLogs: any[] = [];
  searchText = '';
  selectedAction = '';
  loading = false;
  errorMessage = '';

  constructor(private auditLogService: AuditLog) {}

  ngOnInit(): void {
    this.loadLogs();
  }

  loadLogs(): void {
    this.loading = true;
    this.errorMessage = '';

    this.auditLogService.getAllLogs().subscribe({
      next: (data) => {
        this.logs = data;
        this.filterLogs();
        this.loading = false;
      },
      error: () => {
        this.errorMessage = 'Unable to load audit logs. Please check whether the API is running.';
        this.loading = false;
      },
    });
  }

  filterLogs(): void {
    this.filteredLogs = this.logs.filter((log) => {
      const search = this.searchText.toLowerCase();

      const matchesSearch =
        String(log.customerId ?? '')
          .toLowerCase()
          .includes(search) ||
        String(log.action ?? '')
          .toLowerCase()
          .includes(search) ||
        String(log.entityName ?? '')
          .toLowerCase()
          .includes(search) ||
        String(log.description ?? '')
          .toLowerCase()
          .includes(search);

      const matchesAction =
        !this.selectedAction || log.action?.toLowerCase() === this.selectedAction.toLowerCase();

      return matchesSearch && matchesAction;
    });
  }

  get totalLogs(): number {
    return this.logs.length;
  }

  get uniqueActions(): string[] {
    return [...new Set(this.logs.map((log) => log.action).filter((action) => !!action))];
  }
}
