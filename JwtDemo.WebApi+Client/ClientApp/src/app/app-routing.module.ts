import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AdminComponent } from "./admin/admin.component";
import { ClientAreaComponent } from "./client-area/client-area.component";
import { HomeComponent } from "./home/home.component";
import { SignInComponent } from "./sign-in/sign-in.component";
import { SignUpComponent } from "./sign-up/sign-up.component";

const routes: Routes = [
  { path: "", pathMatch: "full", component: HomeComponent },
  { path: "sign-up", pathMatch: "full", component: SignUpComponent },
  { path: "sign-in", pathMatch: "full", component: SignInComponent },
  { path: "admin-panel", pathMatch: "full", component: AdminComponent },
  { path: "client-area", pathMatch: "full", component: ClientAreaComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
