import { AfterViewInit, Component, HostListener, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-qr',
  templateUrl: './qr.component.html',
  styleUrls: ['./qr.component.scss'],
})
export class QRComponent implements OnInit, AfterViewInit {
  localDialogRef;
  shouldCall = false;
  dialogWidth = '50%';
  constructor(
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<any>
  ) {}

  ngOnInit(): void {
    if (window.innerWidth > 993) {
      this.dialogWidth = '50%';
    }
    if (window.innerWidth < 993) {
      this.dialogWidth = '80%';
    }
    if (window.innerWidth < 576) {
      this.dialogWidth = '90%';
    }
  }

  ngAfterViewInit() {
    this.shouldCall = true;
  }

  openDialog(templateRef) {
    this.localDialogRef = this.dialog.open(templateRef, {
      width: this.dialogWidth,
    });
  }

  onClose() {
    this.localDialogRef.close();
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    if (this.shouldCall) this.updateWidthOfDialogue();
  }

  updateWidthOfDialogue() {
    if (window.innerWidth > 993) {
      this.localDialogRef.updateSize('50%', '');
    }
    if (window.innerWidth < 993) {
      this.localDialogRef.updateSize('80%', '');
    }
    if (window.innerWidth < 576) {
      this.localDialogRef.updateSize('90%', '');
    }
  }
}
