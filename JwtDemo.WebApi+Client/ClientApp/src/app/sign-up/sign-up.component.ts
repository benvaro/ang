import { Component, OnInit } from "@angular/core";
import { NgxSpinnerService } from "ngx-spinner";
import { SignUpModel } from "../Models/sign-up.model";

@Component({
  selector: "app-sign-up",
  templateUrl: "./sign-up.component.html",
  styleUrls: ["./sign-up.component.css"],
})
export class SignUpComponent implements OnInit {
  model = new SignUpModel();
  confirmPassword: string;

  constructor(private spinner: NgxSpinnerService) {}

  register() {
    this.spinner.show();
    console.log(this.model);

    setTimeout(() => this.spinner.hide(), 2000);
  }

  ngOnInit() {}
}
