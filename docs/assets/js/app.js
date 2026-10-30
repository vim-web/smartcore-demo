/**
 * SMARTCORE CBS - Modern Static UI Demonstration Scripts
 * Implements harmless local interactivity, tab toggles, modal dialogs,
 * calculations, live search, and demo navigation.
 */

const CBS = {
  // Toast Notification System
  showToast(message, type = 'info', duration = 3500) {
    let container = document.querySelector('.toast-container');
    if (!container) {
      container = document.createElement('div');
      container.className = 'toast-container';
      document.body.appendChild(container);
    }

    const toast = document.createElement('div');
    toast.className = `toast ${type}`;

    const iconSvg = {
      success: `<svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="#10b981" stroke-width="2"><path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/><polyline points="22 4 12 14.01 9 11.01"/></svg>`,
      danger: `<svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="#ef4444" stroke-width="2"><circle cx="12" cy="12" r="10"/><line x1="15" y1="9" x2="9" y2="15"/><line x1="9" y1="9" x2="15" y2="15"/></svg>`,
      warning: `<svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="#f59e0b" stroke-width="2"><path d="M10.29 3.86L1.82 18a2 2 0 0 0 1.71 3h16.94a2 2 0 0 0 1.71-3L13.71 3.86a2 2 0 0 0-3.42 0z"/><line x1="12" y1="9" x2="12" y2="13"/><line x1="12" y1="17" x2="12.01" y2="17"/></svg>`,
      info: `<svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="#1e5bb8" stroke-width="2"><circle cx="12" cy="12" r="10"/><line x1="12" y1="16" x2="12" y2="12"/><line x1="12" y1="8" x2="12.01" y2="8"/></svg>`
    }[type] || '';

    toast.innerHTML = `
      ${iconSvg}
      <div style="flex: 1;">${message}</div>
      <button style="background:none;border:none;color:#94a3b8;cursor:pointer;font-size:16px;" onclick="this.parentElement.remove()">&times;</button>
    `;

    container.appendChild(toast);

    setTimeout(() => {
      toast.style.opacity = '0';
      toast.style.transform = 'translateY(10px)';
      toast.style.transition = 'all 0.25s ease';
      setTimeout(() => toast.remove(), 250);
    }, duration);
  },

  // Modal Control
  openModal(id) {
    const modal = document.getElementById(id);
    if (modal) {
      modal.classList.add('open');
      modal.style.display = 'flex';
      document.body.style.overflow = 'hidden';
    }
  },

  closeModal(id) {
    const modal = document.getElementById(id);
    if (modal) {
      modal.classList.remove('open');
      modal.style.display = 'none';
      document.body.style.overflow = '';
    }
  },

  // Minor Age Checker (CustomerRegister.aspx logic)
  checkAgeMinor() {
    const ageInput = document.getElementById('TxtAge');
    const guardianDiv = document.getElementById('GuardianDiv');
    if (!ageInput || !guardianDiv) return;

    const age = parseInt(ageInput.value, 10);
    if (!isNaN(age) && age < 18 && age > 0) {
      guardianDiv.style.display = 'block';
      CBS.showToast('Minor customer detected (< 18 yrs). Guardian details are mandatory.', 'warning');
    } else {
      guardianDiv.style.display = 'none';
    }
  },

  // Calculate Age from Date of Birth (dd/mm/yyyy or yyyy-mm-dd)
  calculateAge(dobStr) {
    if (!dobStr) return;
    let birthDate;
    if (dobStr.includes('/')) {
      const parts = dobStr.split('/');
      birthDate = new Date(parts[2], parts[1] - 1, parts[0]);
    } else {
      birthDate = new Date(dobStr);
    }

    if (isNaN(birthDate.getTime())) return;

    const today = new Date(2026, 9, 29); // Demo CBS date 29/10/2026
    let age = today.getFullYear() - birthDate.getFullYear();
    const m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }

    const ageInput = document.getElementById('TxtAge');
    if (ageInput) {
      ageInput.value = age >= 0 ? age : 0;
      CBS.checkAgeMinor();
    }
  },

  // Copy Present Address to Permanent Address (Original Update() in CustomerRegister.aspx)
  copyPresentAddress() {
    const mappings = [
      ['TxtPresHouseNo', 'TxtPermHouseNo'],
      ['TxtPresHouseName', 'TxtPermHouseName'],
      ['TxtPresPlace', 'TxtPermPlace'],
      ['TxtPresward', 'TxtPremward'],
      ['TxtPrespancynt', 'TxtPrempancynt'],
      ['txtPresvillage', 'TxtPermvillage'],
      ['TxtPrestaluk', 'TxtPermtaluk'],
      ['TxtPresDist', 'TxtPermDist'],
      ['TxtPresState', 'TxtPermState'],
      ['TxtPresCountry', 'TxtPermCountry'],
      ['TxtPresPin', 'TxtPermPin'],
      ['TxtPresLand', 'TxtPermLand']
    ];

    let copiedCount = 0;
    mappings.forEach(([srcId, destId]) => {
      const src = document.getElementById(srcId);
      const dest = document.getElementById(destId);
      if (src && dest) {
        dest.value = src.value;
        copiedCount++;
      }
    });

    CBS.showToast('Present address successfully copied to Permanent address.', 'success');
  },

  // Deposit Maturity Calculation (DepositMaster.aspx)
  calculateDepositMaturity() {
    const amtInput = document.getElementById('Txt_InitialDeposit');
    const rateInput = document.getElementById('Txt_IntPercent');
    const monthsInput = document.getElementById('Txt_Month');
    const maturityInput = document.getElementById('Txt_MaturityAmt');

    if (!amtInput || !rateInput || !monthsInput || !maturityInput) return;

    const principal = parseFloat(amtInput.value.replace(/,/g, '')) || 0;
    const rate = parseFloat(rateInput.value) || 0;
    const months = parseInt(monthsInput.value, 10) || 0;

    if (principal > 0 && rate > 0 && months > 0) {
      // Quarterly compounding for term deposits
      const years = months / 12;
      const compoundTimes = 4;
      const r = rate / 100;
      const maturity = principal * Math.pow(1 + (r / compoundTimes), compoundTimes * years);
      maturityInput.value = maturity.toLocaleString('en-IN', { maximumFractionDigits: 2, minimumFractionDigits: 2 });
    }
  },

  // Table Search Filter
  initTableSearch() {
    document.querySelectorAll('[data-table-search]').forEach(input => {
      const targetTableId = input.getAttribute('data-table-search');
      const table = document.getElementById(targetTableId);
      if (!table) return;

      input.addEventListener('input', (e) => {
        const query = e.target.value.toLowerCase().trim();
        const rows = table.querySelectorAll('tbody tr');
        let visibleCount = 0;

        rows.forEach(row => {
          const text = row.textContent.toLowerCase();
          if (text.includes(query)) {
            row.style.display = '';
            visibleCount++;
          } else {
            row.style.display = 'none';
          }
        });

        const counter = document.getElementById(`${targetTableId}-count`);
        if (counter) {
          counter.textContent = `${visibleCount} records found`;
        }
      });
    });
  },

  // Ticker Controls
  initTicker() {
    const tickerText = document.querySelector('.cbs-ticker-text');
    const playPauseBtn = document.getElementById('btnTickerPlayPause');
    if (!tickerText || !playPauseBtn) return;

    let isPaused = false;
    playPauseBtn.addEventListener('click', () => {
      isPaused = !isPaused;
      tickerText.style.animationPlayState = isPaused ? 'paused' : 'running';
      playPauseBtn.innerHTML = isPaused ? '&#9658;' : '&#10074;&#10074;';
      playPauseBtn.title = isPaused ? 'Resume Ticker' : 'Pause Ticker';
    });
  },

  // Cash Denomination Calculator (CashDenomination.aspx & DayBeginEnd.aspx)
  calculateDenomination() {
    const denominations = [2000, 500, 200, 100, 50, 20, 10, 5, 2, 1];
    let grandTotal = 0;

    denominations.forEach(denom => {
      const countInput = document.getElementById(`denom_${denom}`);
      const valInput = document.getElementById(`val_${denom}`);
      if (countInput && valInput) {
        const count = parseInt(countInput.value, 10) || 0;
        const total = count * denom;
        valInput.value = total ? total.toLocaleString('en-IN', { minimumFractionDigits: 2 }) : '0.00';
        grandTotal += total;
      }
    });

    const totalPhysical = document.getElementById('totalPhysicalCash');
    if (totalPhysical) {
      totalPhysical.textContent = '₹ ' + grandTotal.toLocaleString('en-IN', { minimumFractionDigits: 2 });
    }

    const sysCashInput = document.getElementById('systemCashBalance');
    const varianceEl = document.getElementById('cashVariance');
    if (sysCashInput && varianceEl) {
      const systemCash = parseFloat(sysCashInput.value.replace(/,/g, '')) || 2485650.00;
      const variance = grandTotal - systemCash;
      varianceEl.textContent = (variance >= 0 ? '+ ₹ ' : '- ₹ ') + Math.abs(variance).toLocaleString('en-IN', { minimumFractionDigits: 2 });
      varianceEl.style.color = variance === 0 ? '#10b981' : (variance > 0 ? '#f59e0b' : '#ef4444');
    }
  },

  // Day End Simulation Runner
  runDayEndSimulation() {
    CBS.openModal('dayEndModal');
    const steps = [
      'Verifying Branch Teller Cash Balances...',
      'Checking Unposted Vouchers & Maker-Checker Queue...',
      'Settling Inter-Branch Clearing & NEFT/RTGS Batches...',
      'Accruing Daily Deposit & Loan Interest...',
      'Balancing General Ledger (Debits = Credits)...',
      'Generating Consolidated Day-End Trial Balance...',
      'Creating Daily Security Snapshot & Database Backup...',
      'Advancing Branch CBS Business Date to 30/10/2026...'
    ];

    const list = document.getElementById('dayEndStepList');
    const progress = document.getElementById('dayEndProgressBar');
    const statusText = document.getElementById('dayEndStatusText');
    const completeBtn = document.getElementById('btnDayEndComplete');

    if (!list || !progress) return;
    list.innerHTML = '';
    completeBtn.style.display = 'none';

    let current = 0;
    const interval = setInterval(() => {
      if (current < steps.length) {
        const li = document.createElement('li');
        li.style.display = 'flex';
        li.style.alignItems = 'center';
        li.style.gap = '8px';
        li.style.fontSize = '12.5px';
        li.style.padding = '4px 0';
        li.innerHTML = `
          <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="#10b981" stroke-width="2.5"><polyline points="20 6 9 17 4 12"/></svg>
          <span>${steps[current]}</span>
        `;
        list.appendChild(li);
        current++;
        const pct = Math.round((current / steps.length) * 100);
        progress.style.width = `${pct}%`;
        statusText.textContent = `Progress: ${pct}% - ${steps[current - 1]}`;
      } else {
        clearInterval(interval);
        statusText.innerHTML = `<strong style="color:#059669;">✓ Day End Process Completed Successfully! Next Working Day Initialized.</strong>`;
        completeBtn.style.display = 'inline-flex';
        CBS.showToast('Day End Process executed successfully for Branch 001.', 'success');
      }
    }, 600);
  },

  // Authorise Transaction in Manager Passing
  authoriseItem(btn, vouNo) {
    const tr = btn.closest('tr');
    if (!tr) return;

    tr.classList.add('highlighted');
    tr.setAttribute('data-status', 'passed');
    const statusCell = tr.querySelector('.passing-status') || tr.querySelector('.status-badge');
    if (statusCell) {
      statusCell.className = 'status-badge status-active';
      statusCell.innerHTML = `Passed`;
    }

    btn.disabled = true;
    btn.textContent = 'Authorised';
    btn.className = 'btn btn-outline btn-sm';

    // Update pending counter
    const pendingCounter = document.getElementById('pendingPassingCount') || document.getElementById('pending-counter-badge');
    if (pendingCounter) {
      let count = parseInt(pendingCounter.textContent.replace(/\D/g, ''), 10);
      if (!isNaN(count) && count > 0) {
        pendingCounter.textContent = 'Pending: ' + (count - 1);
      }
    }

    CBS.showToast(`Voucher #${vouNo} has been verified and authorised by Manager (Demo Mode).`, 'success');
  },

  // Instant Table Filtering by Query String
  filterTableRows(tableId, query) {
    const table = document.getElementById(tableId);
    if (!table) return;
    const q = (query || '').toLowerCase().trim();
    const rows = table.querySelectorAll('tbody tr');
    let visible = 0;

    rows.forEach(row => {
      const text = row.textContent.toLowerCase();
      if (!q || text.includes(q)) {
        row.style.display = '';
        visible++;
      } else {
        row.style.display = 'none';
      }
    });

    const counter = document.getElementById('grid-row-count') || document.getElementById(`${tableId}-count`);
    if (counter) {
      counter.textContent = `Showing ${visible} items`;
    }
  },

  // Clear Form (both direct and with confirm)
  clearForm(formId) {
    const form = document.getElementById(formId);
    if (!form) return;
    form.reset();
    CBS.showToast('Form fields cleared to default demo values.', 'info');
  },

  confirmClearForm(formId) {
    const form = document.getElementById(formId);
    if (!form) return;
    if (confirm('Are you sure you want to clear all form fields? Unsaved inputs will be lost.')) {
      form.reset();
      CBS.showToast('Form has been reset to default state.', 'info');
    }
  },

  // Demo Save Trigger
  demoSave(entityName = 'Record') {
    CBS.showToast(`${entityName} saved successfully in static demo mode (Validation Passed).`, 'success');
  },

  // Sidebar Nav Group Accordion Toggle
  toggleNavGroup(headerEl) {
    const group = headerEl.closest('.cbs-nav-group');
    if (group) {
      group.classList.toggle('collapsed');
    }
  },

  // Filter Sidebar Links
  filterSidebar(query) {
    const q = (query || '').toLowerCase().trim();
    const groups = document.querySelectorAll('.cbs-nav-group');
    groups.forEach(group => {
      const items = group.querySelectorAll('.cbs-nav-item');
      let groupHasMatch = false;
      items.forEach(item => {
        const text = item.textContent.toLowerCase();
        if (!q || text.includes(q)) {
          item.style.display = '';
          groupHasMatch = true;
        } else {
          item.style.display = 'none';
        }
      });
      if (!q) {
        if (!group.dataset.activeGroup) {
          group.classList.add('collapsed');
        } else {
          group.classList.remove('collapsed');
        }
        group.style.display = '';
      } else {
        if (groupHasMatch) {
          group.classList.remove('collapsed');
          group.style.display = '';
        } else {
          group.style.display = 'none';
        }
      }
    });
  },

  // Pending Module Dialog Handler
  showPendingModule(filename, title, category) {
    const titleEl = document.getElementById('pendingModalTitle');
    const fileEl = document.getElementById('pendingModalFile');
    const catEl = document.getElementById('pendingModalCategory');
    if (titleEl) titleEl.textContent = title || filename;
    if (fileEl) fileEl.textContent = filename;
    if (catEl) catEl.textContent = category;
    CBS.openModal('pendingModuleModal');
  }
};

window.toggleNavGroup = (el) => CBS.toggleNavGroup(el);
window.filterSidebar = (q) => CBS.filterSidebar(q);
window.openPendingModal = (fn, title, cat) => CBS.showPendingModule(fn, title, cat);


// Global Initialization on DOM Ready
document.addEventListener('DOMContentLoaded', () => {
  // Mobile Sidebar Toggle
  const toggleBtn = document.querySelector('.cbs-menu-toggle');
  const sidebar = document.querySelector('.cbs-sidebar');
  if (toggleBtn && sidebar) {
    toggleBtn.addEventListener('click', () => {
      sidebar.classList.toggle('open');
    });

    // Close when clicking outside on mobile
    document.addEventListener('click', (e) => {
      if (window.innerWidth <= 1024 && !sidebar.contains(e.target) && !toggleBtn.contains(e.target)) {
        sidebar.classList.remove('open');
      }
    });
  }

  // Close modals when clicking backdrop
  document.querySelectorAll('.cbs-modal-overlay').forEach(modal => {
    modal.addEventListener('click', (e) => {
      if (e.target === modal) {
        modal.classList.remove('open');
        document.body.style.overflow = '';
      }
    });
  });

  // ESC key closes modals
  document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') {
      document.querySelectorAll('.cbs-modal-overlay.open').forEach(modal => {
        modal.classList.remove('open');
        document.body.style.overflow = '';
      });
    }
  });

  // Initialize ticker & table search
  CBS.initTicker();
  CBS.initTableSearch();

  // Attach DOB calculation listener
  const dobInput = document.getElementById('TxtDob');
  if (dobInput) {
    dobInput.addEventListener('change', (e) => CBS.calculateAge(e.target.value));
    dobInput.addEventListener('blur', (e) => CBS.calculateAge(e.target.value));
  }

  // Attach Deposit calculator listeners
  ['Txt_InitialDeposit', 'Txt_IntPercent', 'Txt_Month'].forEach(id => {
    const el = document.getElementById(id);
    if (el) {
      el.addEventListener('input', CBS.calculateDepositMaturity);
    }
  });

  // Cash Denomination auto-calc
  document.querySelectorAll('.denom-input').forEach(input => {
    input.addEventListener('input', CBS.calculateDenomination);
  });
});
