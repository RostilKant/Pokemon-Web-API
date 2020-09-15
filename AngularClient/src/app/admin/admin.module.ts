import {NgModule, Provider} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';

import {AdminLayoutComponent} from './shared/components/admin-layout/admin-layout.component';
import {LoginPageComponent} from './login-page/login-page.component';
import {DashboardPageComponent} from './dashboard-page/dashboard-page.component';
import {CreatePageComponent} from './create-page/create-page.component';
import {EditPageComponent} from './edit-page/edit-page.component';
import {SharedModule} from '../shared/shared.module';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {AuthService} from './shared/services/auth.service';
import {AuthGuard} from './shared/services/auth.guard';
import {PostService} from './shared/services/post.service';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {MyInterceptor} from '../shared/my.interceptor';

const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  multi: true,
  useClass: MyInterceptor
};

@NgModule({
  declarations: [
    AdminLayoutComponent,
    LoginPageComponent,
    DashboardPageComponent,
    CreatePageComponent,
    EditPageComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {
        path: '', component: AdminLayoutComponent, children: [
          {path: '', redirectTo: '/admin/login', pathMatch: 'full'},
          {path: 'login', component: LoginPageComponent},
          {path: 'dashboard', component: DashboardPageComponent, canActivate: [AuthGuard]},
          {path: 'create', component: CreatePageComponent, canActivate: [AuthGuard]},
          {path: 'pokemon/:id/edit', component: EditPageComponent, canActivate: [AuthGuard]}
        ]
      }
    ]),
    SharedModule
  ],
  providers: [
    AuthService,
    AuthGuard,
    PostService,
    INTERCEPTOR_PROVIDER
  ]
})
export class AdminModule {
}
